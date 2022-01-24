using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces.Repository;
using XBank.Repository.Persistence;

namespace XBank.Repository.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly XBankDbContext _dbContext;
        private readonly DbSet<TransactionEntity> _dbSet;

        public TransactionRepository(XBankDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TransactionEntity>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            TransactionEntity entity = await GetAsync(id);
            var response = _dbSet.Remove(entity);
            return response.Entity.Id > 0;
        }

        public async Task<bool> ExistAsync(long id)
        {
            TransactionEntity transactionEntity = await GetAsync(id);
            return transactionEntity.Id > 0;
        }

        public async Task<IEnumerable<TransactionEntity>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TransactionEntity> GetAsync(long id)
        {
            return await _dbSet.FirstOrDefaultAsync(transaction => transaction.Id.Equals(id));
        }

        public async Task<TransactionEntity> PostAsync(TransactionEntity entity)
        {
            var response = await _dbSet.AddAsync(entity);
            return response.Entity;
        }

        public async Task<TransactionEntity> PutAsync(TransactionEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }
    }
}
