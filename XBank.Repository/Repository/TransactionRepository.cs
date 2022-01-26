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

        public async Task<bool> Commit()
        {
            int response = await _dbContext.SaveChangesAsync();
            return response > 0;
        }

        public async Task<bool> DeleteAsync(TransactionEntity entity)
        {
            var response = _dbSet.Remove(entity);
            return response.State == EntityState.Deleted;
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

        public async Task<bool> PostAsync(TransactionEntity entity)
        {
            var response = await _dbSet.AddAsync(entity);
            return response.State == EntityState.Added;
        }

        public async Task<bool> PutAsync(TransactionEntity entity)
        {
            var response = _dbSet.Update(entity);
            return response.State == EntityState.Modified;
        }
    }
}
