using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces;
using XBank.Domain.Interfaces.Repository;
using XBank.Domain.Models.InputModel;

namespace XBank.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<object> DeleteAsync(long id)
        {
            if (id < 1)
                throw new Exception("Id Informado é nulo.");
            if (!await _transactionRepository.ExistAsync(id))
                throw new Exception("Nenhuma trasação encontrada pelo id Informado.");

            bool response = await _transactionRepository.DeleteAsync(id);
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
            TransactionEntity transactionEntity = _mapper.Map<TransactionEntity>(transactionInputModel);
            TransactionEntity response = await _transactionRepository.PostAsync(transactionEntity);

            if (await _transactionRepository.ExistAsync(response.Id))
            {
                bool isCommitted = await _transactionRepository.Commit();
                if (isCommitted)
                    return response;
            }

            throw new Exception("Ocorreu um erro ao criar a transação.");
        }
    }
}
