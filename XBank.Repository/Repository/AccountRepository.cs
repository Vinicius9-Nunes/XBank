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
    public class AccountRepository : IAccountRepository
    {
        private readonly XBankDbContext _dbContext;
        private readonly DbSet<AccountEntity> _dbSet;

        public AccountRepository(XBankDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<AccountEntity>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            AccountEntity entity = await GetAsync(id);
            var response = _dbSet.Remove(entity);
            return response.Entity.Id > 0;
        }

        public async Task<bool> ExistAsync(long id)
        {
            AccountEntity entity = await GetAsync(id);
            return entity?.Id > 0;
        }

        public async Task<IEnumerable<AccountEntity>> GetAsync()
        {
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

        public async Task<AccountEntity> PostAsync(AccountEntity entity)
        {
            var entityResponse = await _dbSet.AddAsync(entity);
            return entityResponse.Entity;
        }

        public async Task<AccountEntity> PutAsync(AccountEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public async Task<bool> Commit()
        {
             int response = await _dbContext.SaveChangesAsync();
            return response > 0;
        }
    }
}
