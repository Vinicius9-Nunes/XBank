using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces.Repository;

namespace XBank.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetByCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> PostAsync(AccountEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> PutAsync(AccountEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
