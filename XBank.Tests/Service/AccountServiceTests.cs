using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Application.Services;
using XBank.Domain.Enums.Account;
using XBank.Domain.Interfaces;
using XBank.Domain.Interfaces.Repository;
using XBank.Domain.Models.DTOs;
using XBank.Domain.Models.InputModel;
using XBank.Tests.Core;
using Xunit;

namespace XBank.Tests.Service
{
    public class AccountServiceTests
    {
        private IAccountService accountService;
        private Faker faker;
        private IMapper mapper;
        private AccountRepositoryMock accountRepositoryMock;
        private string defaultCpf;
        private long defaultId;

        public AccountServiceTests()
        {
            this.mapper = InstanceFactory.GetValidMapper();
            this.accountService = new AccountService(new Mock<IAccountRepository>().Object, mapper);
            this.faker = new Faker("pt_BR");
            defaultCpf = faker.Person.Cpf().RemoveCpfLetters();
            defaultId = faker.Random.Long(1, 10);
            accountRepositoryMock = new AccountRepositoryMock(defaultCpf, defaultId);
        }

        [Fact]
        public async void Add_Money_Invalid_Value()
        {
            string partOfErrorMessage = "maior que zero";
            long id = 1;
            double value = 0;
            Exception exception = await Assert.ThrowsAsync<Exception>(() => accountService.AddMoneyAsync(id, value));
            bool response = exception.Message.Contains(partOfErrorMessage);
            Assert.True(response);
        }

        [Fact]
        public async void Add_Money_Invalid_AccountStatus()
        {
            string defaultCpf = faker.Person.Cpf();
            long defaultId = faker.Random.Long(1, 10);
            double value = 50;
            AccountRepositoryMock accountRepositoryMock = new AccountRepositoryMock(defaultCpf, defaultId, true);
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            await Assert.ThrowsAsync<Exception>(() => _accountService.AddMoneyAsync(defaultId, value));
        }

        [Fact]
        public async void Add_Money_Valid()
        {
            double value = 50;
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            AccountUpdateBalanceDTO updateBalanceDTO = await _accountService.AddMoneyAsync(defaultId, value);
            Assert.NotNull(updateBalanceDTO);
            Assert.True(updateBalanceDTO.HolderCpf == defaultCpf);
        }

        [Fact]
        public async void Delete_Invalid_Id()
        {
            string partOfErrorMessage = "Id informado é nulo.";
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            AccountInputModelDelete modelDelete = new AccountInputModelDelete
            {
                Id = 0,
                AccountStatus = AccountStatus.Active
            };
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _accountService.DeleteAsync(modelDelete));
            bool response = exception.Message.Contains(partOfErrorMessage);
            Assert.True(response);
        }

        [Fact]
        public async void Delete_NonExistent_Id()
        {
            string partOfErrorMessage = "Nenhuma conta encontrada pelo id informado";
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            AccountInputModelDelete modelDelete = new AccountInputModelDelete
            {
                Id = faker.Random.Long(11, 20),
                AccountStatus = AccountStatus.Active
            };
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _accountService.DeleteAsync(modelDelete));
            bool response = exception.Message.Contains(partOfErrorMessage);
            Assert.True(response);
        }

        [Fact]
        public async void Delete_Account()
        {
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            AccountInputModelDelete modelDelete = new AccountInputModelDelete
            {
                Id = defaultId,
                AccountStatus = AccountStatus.Disabled
            };
            bool response = await _accountService.DeleteAsync(modelDelete);
            Assert.False(response);
        }

        [Fact]
        public async void Get_AccountId_By_Cpf()
        {
            string cpfDTO = defaultCpf.RemoveCpfLetters();
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            long response = await _accountService.GetAccountIdByCpfAsync(cpfDTO);
            Assert.True(response == defaultId);
        }

        [Fact]
        public async void Get_Account_By_Cpf()
        {
            string cpfDTO = defaultCpf.RemoveCpfLetters();
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            AccountDTO account = await _accountService.GetByCpfAsync(cpfDTO);
            Assert.NotNull(account);
        }

        [Fact]
        public async void Get_All()
        {
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            IEnumerable<AccountDTO> accounts = await _accountService.GetAsync(false);
            Assert.NotNull(accounts);
        }

        [Fact]
        public async void Get_Account_By_Id()
        {
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            AccountDTO accounts = await _accountService.GetAsync(defaultId);
            Assert.NotNull(accounts);
            Assert.Equal(accounts.Id, defaultId);
        }

        [Fact]
        public async void Add_Account()
        {
            IAccountService _accountService = new AccountService(accountRepositoryMock, mapper);
            AccountInputModelCreate createInput = new AccountInputModelCreate
            {
                HolderCpf = "12345678911",
                HolderName = faker.Person.FirstName,
                DueDate = 15
            };
            AccountCreateDTO accounts = await _accountService.PostAsync(createInput);
            Assert.NotNull(accounts);
        }
    }
}
