using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Account;
using XBank.Domain.Interfaces.Repository;
using XBank.Repository.Persistence;

namespace XBank.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly XBankDbContext _dbContext;
        private readonly DbSet<AccountEntity> _dbSet;

        public AccountRepository(XBankDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<AccountEntity>();
        }

        public async Task<bool> DeleteAsync(AccountEntity entity)
        {
            var response = _dbSet.Remove(entity);
            return response.State == EntityState.Deleted;
        }

        public async Task<bool> ExistAsync(long id)
        {
            AccountEntity entity = await GetAsync(id);
            return entity?.Id > 0;
        }

        public async Task<IEnumerable<AccountEntity>> GetAsync(bool includeDisabled)
        {
            if(!includeDisabled)
                return await _dbSet.Where(account => account.AccountStatus == AccountStatus.Active).ToListAsync();
            else
                return await _dbSet.ToListAsync();
        }

        public async Task<AccountEntity> GetAsync(long id)
        {
            AccountEntity entity = await _dbSet.FirstOrDefaultAsync(account => account.Id.Equals(id));
            return entity;
        }

        public async Task<AccountEntity> GetByCpfAsync(string cpf)
        {
            AccountEntity entity = await _dbSet.FirstOrDefaultAsync(account => account.HolderCpf.Equals(cpf));
            return entity;
        }

        public async Task<bool> PostAsync(AccountEntity entity)
        {
            var response = await _dbSet.AddAsync(entity);
            return response.State == EntityState.Added;
        }

        public async Task<bool> PutAsync(AccountEntity entity)
        {
            var response = _dbSet.Update(entity);
            return response.State == EntityState.Modified;
        }

        public async Task<bool> Commit()
        {
             int response = await _dbContext.SaveChangesAsync();
            return response > 0;
        }
    }
}
