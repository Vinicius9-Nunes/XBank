using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Transaction;

namespace XBank.Domain.Models.DTOs.Transactions
{
    public class TransactionCreateDTO : ModelDTO
    {
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
