using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;

namespace XBank.Domain.Interfaces.Repository
{
    public interface ITransactionRepository : IRepository<TransactionEntity>
    {
        Task<IEnumerable<TransactionEntity>> GetAsync();
    }
}
