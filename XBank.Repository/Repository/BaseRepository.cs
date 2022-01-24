using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces.Repository;

namespace XBank.Repository.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<T> PostAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> PutAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
