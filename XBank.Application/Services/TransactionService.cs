using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XBank.Application.Services.Core;
using XBank.Application.Transaction;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Transaction;
using XBank.Domain.Interfaces;
using XBank.Domain.Interfaces.Core;
using XBank.Domain.Interfaces.Repository;
using XBank.Domain.Models.DTOs.Transactions;
using XBank.Domain.Models.InputModel;
using XBank.Domain.Validators.InputModelsValidators;

namespace XBank.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, IConfiguration configuration)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            if (id < 1)
                throw new Exception("Id Informado é nulo.");
            if (!await _transactionRepository.ExistAsync(id))
                throw new Exception("Nenhuma trasação encontrada pelo id Informado.");
            TransactionEntity transactionEntity = await _transactionRepository.GetAsync(id);
            bool response = await _transactionRepository.DeleteAsync(transactionEntity);
            if (response)
            {
                bool isCommitted = await _transactionRepository.Commit();
                if (isCommitted)
                    return response;
            }

            throw new Exception("Ocorreu um erro ao deletar a transação.");

        }

        public async Task<IEnumerable<TransactionDTO>> GetAsync()
        {
            IEnumerable<TransactionEntity> transactionEntities =  await _transactionRepository.GetAsync();
            return transactionEntities?.Select(transaction => _mapper.Map<TransactionDTO>(transaction));
        }

        public async Task<TransactionDTO> GetAsync(long id)
        {
            if (id < 1)
                throw new ArgumentNullException("Id informado é nulo.");
            if (!await _transactionRepository.ExistAsync(id))
                throw new Exception("Nenhuma transação encontrada pelo id informado");

            TransactionEntity transaction = await _transactionRepository.GetAsync(id);
            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<TransactionCreateDTO> PostAsync(string cpf, TransactionInputModelCreate transactionInputModel)
        {
            if (!cpf.IsValidCPF())
                throw new ArgumentException("O cpf informado é invalido.");
            cpf = cpf.RemoveCpfLetters();

            TransactionEntity transactionEntity = _mapper.Map<TransactionEntity>(transactionInputModel);
            transactionEntity.InitializeTransaction();

            ILocalRequestHttp<Int64> localRequestHttp = new LocalRequestHttp<Int64>();
            string fullUrl = string.Concat(UtilitiesLibrary.GetSectionFromSettings(_configuration, "EndPoints", "BaseEndPointAccount"), "GetAccountIdByCpf/");
            long accountId = await localRequestHttp.Get<long>(fullUrl, cpf);

            if (accountId > 0)
            {
                transactionEntity.UpdateAccountEntityId(accountId);
                BankTransaction bankTransaction = null;

                if (transactionEntity.TransactionType == TransactionType.Credit)
                    bankTransaction = new CreditTransaction(_configuration, _mapper);

                else if (transactionEntity.TransactionType == TransactionType.Debit)
                    bankTransaction = new DebitTransaction(_configuration, _mapper);

                else throw new Exception("Não foi localizado o tipo da transação.");

                bool UpdateAccount = await bankTransaction.MakeTransaction(transactionEntity);
                if (UpdateAccount)
                {
                    bool added = await _transactionRepository.PostAsync(transactionEntity);
                    if (added)
                    {
                        bool isCommitted = await _transactionRepository.Commit();
                        if (isCommitted && await _transactionRepository.ExistAsync(transactionEntity.Id))
                        {
                            TransactionEntity transaction = await _transactionRepository.GetAsync(transactionEntity.Id);
                            return _mapper.Map<TransactionCreateDTO>(transaction);
                        }
                    }
                }
            }

            throw new Exception("Ocorreu um erro ao criar a transação.");
        }
    }
}
