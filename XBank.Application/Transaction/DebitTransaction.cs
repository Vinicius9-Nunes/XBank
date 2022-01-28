using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Application.Services.Core;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces.Core;

namespace XBank.Application.Transaction
{
    public class DebitTransaction : BankTransaction
    {
        private ILocalRequestHttp _localRequestHttp;
        private IConfiguration _configuration;

        public DebitTransaction(IConfiguration configuration) : base()
        {
            _localRequestHttp = new LocalRequestHttp();
            _configuration = configuration;
        }

        public async override Task<bool> MakeTransaction(TransactionEntity transaction)
        {
            AccountEntity account = await GetAccount(transaction.AccountEntityId);
            account = DebitBalance(account, transaction.Amount);
            // Fazer put na api de account
            return true;
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
            string baseUrl = UtilitiesLibrary.GetSectionFromSettings(_configuration, "EndPoints", "BaseEndPointAccount");
            string fullUrl = string.Concat(baseUrl);
            return await _localRequestHttp.Get<AccountEntity>(fullUrl, id.ToString());
        }
    }
}
