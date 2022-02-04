using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Account;
using XBank.Domain.Enums.Transaction;

namespace XBank.Tests.Core
{
    public class Utilities
    {
        public static AccountEntity BuildAccountEntity()
        {
            AccountEntity accountEntity = new Faker<AccountEntity>()
                .RuleFor(account => account.Id, f => f.Random.Long(0, 100))
                .RuleFor(account => account.HolderName, f => f.Person.FullName)
                .RuleFor(account => account.HolderCpf, f => f.Person.Cpf())
                .RuleFor(account => account.DueDate, f => f.Random.Int(1, 28))
                .RuleFor(account => account.AccountStatus, f => f.Random.Enum<AccountStatus>())
                .RuleFor(account => account.Balance, f => f.Random.Double(1, 200));
            return accountEntity;
        }

        public static List<TransactionEntity> BuildTransactionEntityList()
        {
            List<TransactionEntity> transactions = new List<TransactionEntity>();
            for (int i = 0; i < 5; i++)
            {
                TransactionEntity transactionEntity = new Faker<TransactionEntity>()
                .RuleFor(trans => trans.Id, f => f.Random.Long(0, 100))
                .RuleFor(trans => trans.Description, f => f.Random.Words(2))
                .RuleFor(trans => trans.Amount, f => f.Random.Double(1, 200))
                .RuleFor(trans => trans.TransactionDate, f => f.Date.Recent(365))
                .RuleFor(trans => trans.TransactionType, f => f.Random.Enum<TransactionType>())
                .RuleFor(trans => trans.AccountEntityId, f => f.Random.Long(0, 100));
                transactions.Add(transactionEntity);
            }
            return transactions;
        }
    }
}
