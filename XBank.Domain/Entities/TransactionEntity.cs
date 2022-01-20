using System;
using System.Collections.Generic;
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
    }
}
