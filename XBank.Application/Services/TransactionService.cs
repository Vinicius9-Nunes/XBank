using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Interfaces;
using XBank.Domain.Models.InputModel;

namespace XBank.Application.Services
{
    public class TransactionService : ITransactionService
    {
        public Task<object> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<object> PostAsync(string cpf, TransactionInputModelCreate transactionInputModel)
        {
            throw new NotImplementedException();
        }
    }
}
