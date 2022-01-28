using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Transaction;

namespace XBank.Application.Transaction
{
    public abstract class BankTransaction
    {
        public abstract Task<bool> MakeTransaction(TransactionEntity transaction);
        
    }
}
