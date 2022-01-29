using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Transaction;

namespace XBank.Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public string Description { get; private set; }
        public double Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public TransactionType TransactionType { get; private set; }
        [ForeignKey("AccountId")]
        public long AccountEntityId { get; private set; }

        public void UpdateAccountEntityId(long accountId)
        {
            if (accountId > 0)
                AccountEntityId = accountId;
        }
        public void InitializeTransaction()
        {
            DateTime date = DateTime.Now;
            CreatAt = date;
            UpdateAt = date;
            if (Amount < 0)
                Amount = 0;
        }
    }
}
