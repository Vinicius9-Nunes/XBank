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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<object> DeleteAsync(long id)
        {
            if (id < 1)
                throw new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                throw new Exception("Nenhuma conta encontrada pelo id informado");

            AccountEntity accountEntity = await _accountRepository.GetAsync(id);

            bool response = await _accountRepository.DeleteAsync(accountEntity);
            if (response)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted)
                    return response;
            }

            throw new Exception("Ocorreu um erro ao deletar a conta.");
        }

        public async Task<long> GetAccountIdByCpfAsync(string cpf)
        {
            AccountEntity accountEntity = await GetByCpfAsync(cpf) as AccountEntity;
            return accountEntity.Id;
        }

        public async Task<object> GetAsync()
        {
            return await _accountRepository.GetAsync();
        }

        public async Task<object> GetAsync(long id)
        {
            if (id < 1)
                throw new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                throw new Exception("Nenhuma conta encontrada pelo id informado");

            return await _accountRepository.GetAsync(id);
        }

        public async Task<object> GetByCpfAsync(string cpf)
        {
            cpf = cpf.Trim();

            //if (cpf.Length != 11)
            //    throw new Exception("CPF invalido, favor validar se foi informado somente os números.");

            AccountEntity accountEntity = await _accountRepository.GetByCpfAsync(cpf);
            if (accountEntity == null || accountEntity?.Id < 1)
                throw new Exception("Nenhuma conta encontrada pelo CPF informado.");

            return accountEntity;
        }



        public async Task<object> PostAsync(AccountInputModelCreate accountInputModel)
        {
            AccountEntity accountEntity = _mapper.Map<AccountEntity>(accountInputModel);
            bool added = await _accountRepository.PostAsync(accountEntity);

            if (added)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted && await _accountRepository.ExistAsync(accountEntity.Id))
                    return await _accountRepository.GetAsync(accountEntity.Id);
            }

            throw new Exception("Ocorreu um erro ao criar a conta.");
        }

        public async Task<object> PutAsync(long id, AccountInputModelUpdate accountInputModel)
        {
            if (id < 1)
                throw new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                throw new Exception("Nenhuma conta encontrada pelo id informado");

            AccountEntity accountEntity = await _accountRepository.GetAsync(id);
            accountEntity.Update(accountInputModel);
            bool modified = await _accountRepository.PutAsync(accountEntity);
            if (modified)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted && await _accountRepository.ExistAsync(accountEntity.Id))
                    return await _accountRepository.GetAsync(accountEntity.Id);
            }

            throw new Exception("Ocorreu um erro ao atualizar a conta.");
        }
    }
}
