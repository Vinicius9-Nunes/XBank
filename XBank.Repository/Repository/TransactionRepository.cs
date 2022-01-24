using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces.Repository;

namespace XBank.Repository.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionEntity> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransactionEntity> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionEntity> PostAsync(TransactionEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionEntity> PutAsync(TransactionEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
