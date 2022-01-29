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
using XBank.Domain.Enums.Account;
using XBank.Domain.Interfaces.Core;
using XBank.Domain.Models.DTOs;
using XBank.Domain.Models.InputModel;

namespace XBank.Application.Transaction
{
    public class DebitTransaction : BankTransaction
    {
        public DebitTransaction(IConfiguration configuration, IMapper mapper) : base(configuration, mapper) {}

        public async override Task<bool> MakeTransaction(TransactionEntity transaction)
        {
            AccountDTO account = await GetAccount(transaction.AccountEntityId);
            if(account.AccountStatus != AccountStatus.Active)
                throw new Exception($"Não é possivel atualizar a conta pois a mesma esta com status de {account.AccountStatus.ToString()}.");

            account = DebitBalance(account, transaction.Amount);
            AccountInputModelDebitTransaction accountInputModelDebit = _mapper.Map<AccountInputModelDebitTransaction>(account);
            UpdateAccountDebitDTO updatedAccount = await UpdateDebitTransactionAccount(accountInputModelDebit);
            return updatedAccount?.Id == transaction.AccountEntityId;
        }
        private AccountDTO DebitBalance(AccountDTO accountDTO, double value)
        {
            if (!CanDebit(accountDTO, value))
                throw new Exception("Você não possui saldo suficiente para realizar essa transação.");

            accountDTO.Debit(value);
            return accountDTO;
        }
        private bool CanDebit(AccountDTO accountDTO, double value)
        {
            double subtractionValue = accountDTO.Balance - value;
            return subtractionValue >= 0;
        }
        private async Task<UpdateAccountDebitDTO> UpdateDebitTransactionAccount(AccountInputModelDebitTransaction accountInputModelDebit)
        {
            ILocalRequestHttp<ModelDTO> localRequestHttp = new LocalRequestHttp<ModelDTO>();
            string baseUrl = UtilitiesLibrary.GetSectionFromSettings(_configuration, "EndPoints", "BaseEndPointAccount");
            string fullUrl = string.Concat(baseUrl, "UpdateAccountTransaction");
            UpdateAccountDebitDTO updateAccountDebitDTO = await localRequestHttp.Put<UpdateAccountDebitDTO>(fullUrl, accountInputModelDebit);
            return updateAccountDebitDTO;
        }
    }
}
