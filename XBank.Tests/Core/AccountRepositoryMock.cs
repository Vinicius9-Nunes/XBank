using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Account;
using XBank.Domain.Interfaces.Repository;

namespace XBank.Tests.Core
{
    public class AccountRepositoryMock : IAccountRepository
    {
        private string defaultCpf;
        private long defaultId;
        private AccountStatus defaultAccountStatus;
        private Faker faker;
        public AccountRepositoryMock(string defaultCpf, long defaultId, bool notActive = false)
        {
            this.defaultCpf = defaultCpf;
            this.defaultId = defaultId;
            defaultAccountStatus = notActive ? AccountStatus.Disabled : AccountStatus.Active;
            this.faker = new Faker("pt_BR");
        }
        public async Task<bool> Commit()
        {
            return true;
        }

        public async Task<bool> DeleteAsync(AccountEntity entity)
        {
            return true;
        }

        public async Task<bool> ExistAsync(long id)
        {
            AccountEntity accountEntity = await GetAsync(id);
            if (id == 0)
                return true;
            return accountEntity.Id == id;
        }

        public async Task<IEnumerable<AccountEntity>> GetAsync(bool includeDisabled)
        {
            List<AccountEntity> accounts = new List<AccountEntity>();
            accounts.Add(
                        new AccountEntity(defaultId,
                            faker.Person.FullName,
                            defaultCpf,
                            faker.Random.Double(1, 100),
                            faker.Random.Int(1, 28),
                            defaultAccountStatus));

            return accounts.Count > 0 && accounts[0].Id == defaultId ? accounts : null;
        }

        public async Task<AccountEntity> GetAsync(long id)
        {
            AccountEntity accountEntity = new AccountEntity(defaultId,
                faker.Person.FullName,
                defaultCpf,
                faker.Random.Double(1, 100),
                faker.Random.Int(1, 28),
                defaultAccountStatus);
            return accountEntity.Id == defaultId ? accountEntity : null;
        }

        public async Task<AccountEntity> GetByCpfAsync(string cpf)
        {
            AccountEntity accountEntity = new AccountEntity(defaultId,
                faker.Person.FullName,
                defaultCpf,
                faker.Random.Double(1, 100),
                faker.Random.Int(1, 28),
                defaultAccountStatus);
            return accountEntity.HolderCpf == cpf ? accountEntity : null;
        }

        public async Task<bool> PostAsync(AccountEntity entity)
        {
            return entity.Id != defaultId ? true : false;
        }

        public async Task<bool> PutAsync(AccountEntity entity)
        {
            AccountEntity accountEntity = await GetAsync(entity.Id);
            return !(entity.HolderName == accountEntity.HolderName
                    && entity.Balance == accountEntity.Balance
                    && entity.AccountStatus == accountEntity.AccountStatus);
        }
    }
}
