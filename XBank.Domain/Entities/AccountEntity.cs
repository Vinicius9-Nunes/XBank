using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Account;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Entities
{
    public class AccountEntity : BaseEntity
    {
        public string HolderName { get; private set; }
        public string HolderCpf { get; private set; }
        public double Balance { get; private set; }
        public int DueDate { get; private set; }
        public AccountStatus AccountStatus { get; private set; }

        public IEnumerable<TransactionEntity> Transactions { get; private set; }

        public AccountEntity Update(AccountInputModelUpdate accountInputModel)
        {
            if (accountInputModel.DueDate > 0)
                DueDate = accountInputModel.DueDate;
            if (!string.IsNullOrEmpty(accountInputModel.HolderName))
                HolderName = accountInputModel.HolderName;
            UpdateAt = DateTime.Now;

            return this;
        }
        public AccountEntity UpdateDebitTransaction(AccountInputModelDebitTransaction accountInputModel)
        {
            if (Id == accountInputModel.Id)
            {
                Balance = accountInputModel.Balance;
                UpdateAt = accountInputModel.UpdateAt;
                return this;
            }
            else
                throw new Exception("O id informado é divergente do conta atual.");

        }

        public void InitializeAccount()
        {
            DateTime date = DateTime.Now;
            CreatAt = date;
            UpdateAt = date;
            AccountStatus = AccountStatus.Active;

            if (!(Balance > 0))
                Balance = 0;
        }

        public void Debit(double value)
        {
            Balance = Balance - value;
            UpdateAt = DateTime.Now;
        }
    }
}
