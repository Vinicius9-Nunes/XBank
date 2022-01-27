using AutoMapper;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces;
using XBank.Domain.Interfaces.Repository;
using XBank.Domain.Models.InputModel;
using XBank.Domain.Validators.InputModelsValidators;

namespace XBank.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly TransactionInputModelCreateValidator _transactionValidation;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _transactionValidation = new TransactionInputModelCreateValidator();
        }

        public async Task<object> DeleteAsync(long id)
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

        public async Task<object> GetAsync()
        {
            return await _transactionRepository.GetAsync();
        }

        public async Task<object> GetAsync(long id)
        {
            if (id < 1)
                throw new ArgumentNullException("Id informado é nulo.");
            if (!await _transactionRepository.ExistAsync(id))
                throw new Exception("Nenhuma transação encontrada pelo id informado");

            return await _transactionRepository.GetAsync(id);
        }

        public async Task<object> PostAsync(string cpf, TransactionInputModelCreate transactionInputModel)
        {
            if(!cpf.IsValidCPF())
                throw new ArgumentException("O cpf informado é invalido.");

            TransactionEntity transactionEntity = _mapper.Map<TransactionEntity>(transactionInputModel);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(@$"https://localhost:44393/api/Account/GetAccountIdByCpf/{cpf.RemoveCpfLetters()}");
            if (response.IsSuccessStatusCode)
            {
                string accountIdResponse = await response.Content.ReadAsStringAsync();
                long accountId = new long();
                long.TryParse(accountIdResponse, out accountId);
                if(accountId > 0)
                {
                    transactionEntity.UpdateAccountEntityId(accountId);
                    bool added = await _transactionRepository.PostAsync(transactionEntity);
                    if (added)
                    {
                        bool isCommitted = await _transactionRepository.Commit();
                        if (isCommitted && await _transactionRepository.ExistAsync(transactionEntity.Id))
                            return await _transactionRepository.GetAsync(transactionEntity.Id);
                    }

                }
            }
            else
            {
                throw new Exception("Ocorreu um erro ao recuperar AccountId.");
            }
            throw new Exception("Ocorreu um erro ao criar a transação.");
        }
    }
}
