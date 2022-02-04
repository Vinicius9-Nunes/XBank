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
        public AccountEntity(long id, string holderName, string holderCpf, double balance, int dueDate, AccountStatus accountStatus)
        {
            Id = id;
            HolderName = holderName;
            HolderCpf = holderCpf;
            Balance = balance;
            DueDate = dueDate;
            AccountStatus = accountStatus;
        }
        public AccountEntity()
        {

        }

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
        public AccountEntity UpdateBalance(double value)
        {
            if(value > 0)
            {
                Balance += value;
                UpdateAt = DateTime.Now;
                return this;
            }

            throw new Exception("Valor deve ser maior que zero.");
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

        public void Disabled()
        {
            if (AccountStatus != AccountStatus.Disabled)
            {
                AccountStatus = AccountStatus.Disabled;
                UpdateAt = DateTime.Now;
            }
            else throw new Exception($"Essa conta já esta com o status {AccountStatus.ToString()}");
        }
        public void Suspended()
        {
            if (AccountStatus != AccountStatus.Suspended)
            {
                AccountStatus = AccountStatus.Suspended;
                UpdateAt = DateTime.Now;
            }
            else throw new Exception($"Essa conta já esta com o status {AccountStatus.ToString()}");
        }
        public void Active()
        {
            if (AccountStatus != AccountStatus.Active)
            {
                AccountStatus = AccountStatus.Active;
                UpdateAt = DateTime.Now;
            }
            else throw new Exception($"Essa conta já esta com o status {AccountStatus.ToString()}");
        }
    }
}
