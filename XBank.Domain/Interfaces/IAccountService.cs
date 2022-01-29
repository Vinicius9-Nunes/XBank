using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Models.DTOs;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAsync();
        Task<AccountDTO> GetAsync(long id);
        Task<AccountDTO> GetByCpfAsync(string cpf);
        Task<long> GetAccountIdByCpfAsync(string cpf);
        Task<AccountCreateDTO> PostAsync(AccountInputModelCreate accountInputModel);
        Task<AccountUpdateDTO> PutAsync(long id, AccountInputModelUpdate accountInputModel);
        Task<UpdateAccountDebitDTO> UpdateDebitAccountAsync(AccountInputModelDebitTransaction accountInputModel);
        Task<bool> DeleteAsync(long id);

    }
}
