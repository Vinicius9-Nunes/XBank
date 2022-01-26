using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<object> GetAsync();
        Task<object> GetAsync(long id);
        Task<object> GetByCpfAsync(string cpf);
        Task<long> GetAccountIdByCpfAsync(string cpf);
        Task<object> PostAsync(AccountInputModelCreate accountInputModel);
        Task<object> PutAsync(long id, AccountInputModelUpdate accountInputModel);
        Task<object> DeleteAsync(long id);

    }
}
