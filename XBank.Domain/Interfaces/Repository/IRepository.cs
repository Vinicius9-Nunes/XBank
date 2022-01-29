using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;

namespace XBank.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(long id);
        Task<bool> ExistAsync(long id);
        Task<bool> PostAsync(T entity);
        Task<bool> PutAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> Commit();
    }
}
