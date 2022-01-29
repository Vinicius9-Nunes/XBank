using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XBank.Application.Services.Core;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces.Core;
using XBank.Domain.Models.InputModel;

namespace XBank.Application.Transaction
{
    public class DebitTransaction : BankTransaction
    {
        private IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DebitTransaction(IConfiguration configuration, IMapper mapper) : base()
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async override Task<bool> MakeTransaction(TransactionEntity transaction)
        {
            AccountEntity account = await GetAccount(transaction.AccountEntityId);
            account = DebitBalance(account, transaction.Amount);
            AccountInputModelDebitTransaction accountInputModelDebit = _mapper.Map<AccountInputModelDebitTransaction>(account);
            AccountEntity updatedAccount = await UpdateDebitTransactionAccount(accountInputModelDebit);
            return updatedAccount?.Id == transaction.AccountEntityId;
        }
        private AccountEntity DebitBalance(AccountEntity accountEntity, double value)
        {
            if (!CanDebit(accountEntity, value))
                throw new Exception("Você não possui saldo suficiente para realizar essa transação.");

            accountEntity.Debit(value);
            return accountEntity;
        }
        private bool CanDebit(AccountEntity accountEntity, double value)
        {
            double subtractionValue = accountEntity.Balance - value;
            return subtractionValue >= 0;
        }
        private async Task<AccountEntity> GetAccount(long id)
        {
            ILocalRequestHttp<BaseEntity> localRequestHttp = new LocalRequestHttp<BaseEntity>();
            string baseUrl = UtilitiesLibrary.GetSectionFromSettings(_configuration, "EndPoints", "BaseEndPointAccount");
            string fullUrl = string.Concat(baseUrl);
            return await localRequestHttp.Get<AccountEntity>(fullUrl, id.ToString());
        }
        private async Task<AccountEntity> UpdateDebitTransactionAccount(AccountInputModelDebitTransaction accountInputModelDebit)
        {
            ILocalRequestHttp<BaseEntity> localRequestHttp = new LocalRequestHttp<BaseEntity>();
            string baseUrl = UtilitiesLibrary.GetSectionFromSettings(_configuration, "EndPoints", "BaseEndPointAccount");
            string fullUrl = string.Concat(baseUrl, "UpdateAccountTransaction");
            AccountEntity updatedAccount = await localRequestHttp.Put<AccountEntity>(fullUrl, accountInputModelDebit);
            return updatedAccount;
        }
    }
}
