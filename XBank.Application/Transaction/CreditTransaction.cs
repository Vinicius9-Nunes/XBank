using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;

namespace XBank.Application.Transaction
{
    public class CreditTransaction : BankTransaction
    {
        public async override Task<bool> MakeTransaction(TransactionEntity transaction)
        {
            // Alguma lógica para popular serviços dessa transação.
            return true;
        }
    }
}
