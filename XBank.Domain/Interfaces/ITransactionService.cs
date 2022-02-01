using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Models.DTOs.Transactions;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAsync();
        Task<TransactionDTO> GetAsync(long id);
        Task<TransactionCreateDTO> PostAsync(string cpf, TransactionInputModelCreate transactionInputModel);
        Task<bool> DeleteAsync(long id);
    }
}
