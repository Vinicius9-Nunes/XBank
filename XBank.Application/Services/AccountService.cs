using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Interfaces;
using XBank.Domain.Interfaces.Repository;
using XBank.Domain.Models.InputModel;

namespace XBank.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public Task<object> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync()
        {
            throw new NotImplementedException()
        }

        public Task<object> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetByCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<object> PostAsync(AccountInputModelCreate accountInputModel)
        {
            throw new NotImplementedException();
        }

        public Task<object> PutAsync(long id, AccountInputModelUpdate accountInputModel)
        {
            throw new NotImplementedException();
        }
    }
}
