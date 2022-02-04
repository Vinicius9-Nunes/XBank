using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Transaction;
using XBank.Domain.Interfaces.Repository;

namespace XBank.Tests.Core
{
    public class TransactionRepositoryMock : ITransactionRepository
    {
        private long defaultId;
        private Faker faker;
        public TransactionRepositoryMock(long defaultId)
        {
            this.defaultId = defaultId;
            this.faker = new Faker("pt_BR");
        }
        
        public async Task<bool> Commit()
        {
            return true;
        }

        public async Task<bool> DeleteAsync(TransactionEntity entity)
        {
            return true;
        }

        public async Task<bool> ExistAsync(long id)
        {
            if (id == 0)
                return true;
            return defaultId == id;
        }

        public async Task<IEnumerable<TransactionEntity>> GetAsync()
        {
            return Utilities.BuildTransactionEntityList();
        }

        public async Task<TransactionEntity> GetAsync(long id)
        {
            TransactionEntity transactionEntity = new Faker<TransactionEntity>()
                .RuleFor(trans => trans.Id, f => f.Random.Long(defaultId - 1, defaultId + 1))
                .RuleFor(trans => trans.Description, f => f.Random.Words(2))
                .RuleFor(trans => trans.Amount, f => f.Random.Double(1, 200))
                .RuleFor(trans => trans.TransactionDate, f => f.Date.Recent(365))
                .RuleFor(trans => trans.TransactionType, f => f.Random.Enum<TransactionType>())
                .RuleFor(trans => trans.AccountEntityId, f => f.Random.Long(0, 100));
            if (id == 0)
                return null;
            return transactionEntity;
        }

        public async Task<bool> PostAsync(TransactionEntity entity)
        {
            return true;
        }

        public async Task<bool> PutAsync(TransactionEntity entity)
        {
            return true;
        }
    }
}
