using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Interfaces
{
    public interface ITransactionService
    {
        Task<object> GetAsync();
        Task<object> GetAsync(long id);
        Task<object> PostAsync(string cpf, TransactionInputModelCreate transactionInputModel);
        Task<object> DeleteAsync(long id);
    }
}
