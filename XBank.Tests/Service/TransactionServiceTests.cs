using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Application.Services;
using XBank.Domain.Interfaces;
using XBank.Domain.Interfaces.Repository;
using XBank.Domain.Models.DTOs.Transactions;
using XBank.Tests.Core;
using Xunit;

namespace XBank.Tests.Service
{
    public class TransactionServiceTests
    {
        private ITransactionService transactionService;
        private Faker faker;
        private IMapper mapper;
        private TransactionRepositoryMock transactionRepositoryMock;
        private long defaultId;

        public TransactionServiceTests()
        {
            this.mapper = InstanceFactory.GetValidMapper();
            this.transactionService = new TransactionService(new Mock<ITransactionRepository>().Object, mapper, new Mock<IConfiguration>().Object);
            this.faker = new Faker("pt_BR");
            defaultId = faker.Random.Long(1, 10);
            transactionRepositoryMock = new TransactionRepositoryMock(defaultId);
        }

        [Fact]
        public async void Delete_Transaction()
        {
            transactionRepositoryMock = new TransactionRepositoryMock(defaultId);
            ITransactionService _transactionService = new TransactionService(transactionRepositoryMock, mapper, new Mock<IConfiguration>().Object);
            bool response = await _transactionService.DeleteAsync(defaultId);
            Assert.True(response);
        }

        [Fact]
        public async void Delete_Invalid_TransactionId()
        {
            string messageError = "Nenhuma trasação encontrada pelo id Informado.";
            ITransactionService _transactionService = new TransactionService(transactionRepositoryMock, mapper, new Mock<IConfiguration>().Object);
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _transactionService.DeleteAsync(faker.Random.Long(50, 60)));
            bool response = exception.Message.Contains(messageError);
            Assert.True(response);
        }

        [Fact]
        public async void Get_All()
        {
            transactionRepositoryMock = new TransactionRepositoryMock(defaultId);
            ITransactionService _transactionService = new TransactionService(transactionRepositoryMock, mapper, new Mock<IConfiguration>().Object);
            IEnumerable<TransactionDTO> transactions = await _transactionService.GetAsync();
            Assert.True(transactions.Any());
            Assert.True(transactions.ToList().Count > 0);
        }

        [Fact]
        public async void Get_By_Id()
        {
            transactionRepositoryMock = new TransactionRepositoryMock(defaultId);
            ITransactionService _transactionService = new TransactionService(transactionRepositoryMock, mapper, new Mock<IConfiguration>().Object);
            TransactionDTO transaction = await _transactionService.GetAsync(defaultId);
            Assert.NotNull(transaction);
        }

        [Fact]
        public async void Get_By_Invalid_Id()
        {
            transactionRepositoryMock = new TransactionRepositoryMock(defaultId);
            string messageError = "Nenhuma transação encontrada pelo id informado";
            ITransactionService _transactionService = new TransactionService(transactionRepositoryMock, mapper, new Mock<IConfiguration>().Object);
            Exception exception =  await Assert.ThrowsAsync<Exception>(() => _transactionService.GetAsync(faker.Random.Long(50, 60)));
            bool response = exception.Message.Contains(messageError);
            Assert.True(response);
        }
    }
}
