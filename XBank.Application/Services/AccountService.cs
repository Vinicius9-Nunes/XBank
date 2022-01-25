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
                return new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                return new Exception("Nenhuma conta encontrada pelo id informado");

            bool response = await _accountRepository.DeleteAsync(id);
            return response;

        }

        public async Task<object> GetAsync()
        {
            return await _accountRepository.GetAsync();
        }

        public async Task<object> GetAsync(long id)
        {
            if (id < 1)
                return new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                return new Exception("Nenhuma conta encontrada pelo id informado");

            return await _accountRepository.GetAsync(id);
        }

        public async Task<object> GetByCpfAsync(string cpf)
        {
            cpf = cpf.Trim();

            if (cpf.Length != 11)
                return new Exception("CPF invalido, favor validar se foi informado somente os números.");

            AccountEntity accountEntity = await _accountRepository.GetByCpfAsync(cpf);
            if (accountEntity == null || accountEntity?.Id < 1)
                return new Exception("Nenhuma conta encontrada pelo CPF informado.");

            return accountEntity;
        }

        public async Task<object> PostAsync(AccountInputModelCreate accountInputModel)
        {
            AccountEntity accountEntity = _mapper.Map<AccountEntity>(accountInputModel);
            AccountEntity response = await _accountRepository.PostAsync(accountEntity);

            if (await _accountRepository.ExistAsync(response.Id))
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted)
                    return response;                    
            }

            return new Exception("Ocorreu um erro ao criar a conta.");
        }

        public async Task<object> PutAsync(long id, AccountInputModelUpdate accountInputModel)
        {
            if(id < 1)
                return new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                return new Exception("Nenhuma conta encontrada pelo id informado");

            AccountEntity accountEntity = await _accountRepository.GetAsync(id);
            AccountEntity respose = await _accountRepository.PutAsync(accountEntity);
            if(respose.Id > 0)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted)
                    return respose;
            }

            return new Exception("Ocorreu um erro ao criar a conta.");
        }
    }
}
