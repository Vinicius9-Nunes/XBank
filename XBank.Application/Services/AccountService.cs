using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Account;
using XBank.Domain.Interfaces;
using XBank.Domain.Interfaces.Repository;
using XBank.Domain.Models.DTOs;
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

        public async Task<AccountUpdateBalanceDTO> AddMoneyAsync(long id, double value)
        {
            if (value <= 0)
                throw new Exception("O Valor deve ser maior que zero");
            if (!await _accountRepository.ExistAsync(id))
                throw new Exception("Não foi localizado uma conta pelo id informado.");

            AccountEntity accountEntity = await _accountRepository.GetAsync(id);
            if (accountEntity.AccountStatus != AccountStatus.Active)
                throw new Exception($"Não é possivel atualizar a conta pois a mesma esta com status de {accountEntity.AccountStatus.ToString()}.");

            accountEntity.UpdateBalance(value);
            bool modified = await _accountRepository.PutAsync(accountEntity);
            if (modified)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted && await _accountRepository.ExistAsync(accountEntity.Id))
                {
                    AccountEntity response = await _accountRepository.GetAsync(accountEntity.Id);
                    return _mapper.Map<AccountUpdateBalanceDTO>(response);
                }
            }

            throw new Exception("Ocorreu um erro ao adicionar dinheiro na conta.");
        }

        public async Task<bool> DeleteAsync(AccountInputModelDelete accountInputModelDelete)
        {
            if (accountInputModelDelete.Id < 1)
                throw new Exception("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(accountInputModelDelete.Id))
                throw new Exception("Nenhuma conta encontrada pelo id informado");

            AccountEntity accountEntity = await _accountRepository.GetAsync(accountInputModelDelete.Id);

            if (accountInputModelDelete.AccountStatus == AccountStatus.Disabled)
                accountEntity.Disabled();
            else if (accountInputModelDelete.AccountStatus == AccountStatus.Suspended)
                accountEntity.Suspended();
            else if(accountInputModelDelete.AccountStatus == AccountStatus.Active)
                accountEntity.Active();

            bool response = await _accountRepository.PutAsync(accountEntity);
            if (response)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted)
                {
                    AccountEntity entity = await _accountRepository.GetAsync(accountEntity.Id);
                    return entity.AccountStatus == accountInputModelDelete.AccountStatus;
                }
            }

            throw new Exception("Ocorreu um erro ao deletar a conta.");
        }

        public async Task<long> GetAccountIdByCpfAsync(string cpf)
        {
            cpf = cpf.Trim().RemoveCpfLetters();
            if (!cpf.IsValidCPF())
                throw new Exception("Cpf infomado é inválido");

            AccountEntity accountEntity = await _accountRepository.GetByCpfAsync(cpf);
            if (accountEntity == null || accountEntity?.Id < 1)
                throw new Exception("Nenhuma conta encontrada pelo CPF informado.");

            return accountEntity.Id;
        }

        public async Task<IEnumerable<AccountDTO>> GetAsync(bool includeDisabled)
        {
            IEnumerable<AccountEntity> accountEntities = await _accountRepository.GetAsync(includeDisabled);
            if (accountEntities.ToList().Count > 0)
            {
                IEnumerable<AccountDTO> accountEntitiesDTO = accountEntities.Select(account => _mapper.Map<AccountDTO>(account));
                return accountEntitiesDTO;
            }
            else return null;

        }

        public async Task<AccountDTO> GetAsync(long id)
        {
            if (id < 1)
                throw new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                throw new Exception("Nenhuma conta encontrada pelo id informado");

            AccountEntity accountEntity = await _accountRepository.GetAsync(id);
            return _mapper.Map<AccountDTO>(accountEntity);
        }

        public async Task<AccountDTO> GetByCpfAsync(string cpf)
        {
            cpf = cpf.Trim().RemoveCpfLetters();
            if (!cpf.IsValidCPF())
                throw new Exception("Cpf infomado é inválido");

            AccountEntity accountEntity = await _accountRepository.GetByCpfAsync(cpf);
            if (accountEntity == null || accountEntity?.Id < 1)
                throw new Exception("Nenhuma conta encontrada pelo CPF informado.");

            return _mapper.Map<AccountDTO>(accountEntity);
        }

        public async Task<AccountCreateDTO> PostAsync(AccountInputModelCreate accountInputModel)
        {
            accountInputModel.RemoveCpfLetters();
            AccountEntity account = await _accountRepository.GetByCpfAsync(accountInputModel.HolderCpf);
            if (account != null || account?.Id > 0)
                throw new Exception("Já existe uma conta vinculada a esse CPF.");

            AccountEntity accountEntity = _mapper.Map<AccountEntity>(accountInputModel);
            accountEntity.InitializeAccount();
            bool added = await _accountRepository.PostAsync(accountEntity);

            if (added)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted && await _accountRepository.ExistAsync(accountEntity.Id))
                {
                    AccountEntity response = await _accountRepository.GetAsync(accountEntity.Id);
                    return _mapper.Map<AccountCreateDTO>(response);
                }
            }

            throw new Exception("Ocorreu um erro ao criar a conta.");
        }

        public async Task<AccountUpdateDTO> PutAsync(long id, AccountInputModelUpdate accountInputModel)
        {
            if (id < 1)
                throw new ArgumentNullException("Id informado é nulo.");
            if (!await _accountRepository.ExistAsync(id))
                throw new Exception("Nenhuma conta encontrada pelo id informado");

            AccountEntity accountEntity = await _accountRepository.GetAsync(id);

            if (accountEntity.AccountStatus != AccountStatus.Active)
                throw new Exception($"Não é possivel atualizar a conta pois a mesma esta com status de {accountEntity.AccountStatus.ToString()}.");
            accountEntity.Update(accountInputModel);
            bool modified = await _accountRepository.PutAsync(accountEntity);
            if (modified)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted && await _accountRepository.ExistAsync(accountEntity.Id))
                {
                    AccountEntity response = await _accountRepository.GetAsync(accountEntity.Id);
                    return _mapper.Map<AccountUpdateDTO>(response);
                }
            }

            throw new Exception("Ocorreu um erro ao atualizar a conta.");
        }

        public async Task<UpdateAccountDebitDTO> UpdateDebitAccountAsync(AccountInputModelDebitTransaction accountInputModel)
        {
            if (!await _accountRepository.ExistAsync(accountInputModel.Id))
                throw new Exception("Não existe um conta com o id informado.");

            AccountEntity accountEntity = await _accountRepository.GetAsync(accountInputModel.Id);

            if (accountEntity.AccountStatus != AccountStatus.Active)
                throw new Exception($"Não é possivel atualizar a conta pois a mesma esta com status de {accountEntity.AccountStatus.ToString()}.");

            accountEntity.UpdateDebitTransaction(accountInputModel);

            bool modified = await _accountRepository.PutAsync(accountEntity);
            if (modified)
            {
                bool isCommitted = await _accountRepository.Commit();
                if (isCommitted && await _accountRepository.ExistAsync(accountEntity.Id))
                {
                    AccountEntity response = await _accountRepository.GetAsync(accountEntity.Id);
                    return _mapper.Map<UpdateAccountDebitDTO>(response);
                }
            }

            throw new Exception("Ocorreu um erro ao atualizar a conta.");
        }
    }
}
