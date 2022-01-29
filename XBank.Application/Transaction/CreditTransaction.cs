using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Account;
using XBank.Domain.Models.DTOs;

namespace XBank.Application.Transaction
{
    public class CreditTransaction : BankTransaction
    {
        public CreditTransaction(IConfiguration configuration, IMapper mapper) : base(configuration, mapper) { }
        public async override Task<bool> MakeTransaction(TransactionEntity transaction)
        {
            AccountDTO account = await GetAccount(transaction.AccountEntityId);
            if (account.AccountStatus != AccountStatus.Active)
                throw new Exception($"Não é possivel atualizar a conta pois a mesma esta com status de {account.AccountStatus.ToString()}.");
            // Alguma lógica para popular serviços dessa transação.
            return true;
        }
    }
}
