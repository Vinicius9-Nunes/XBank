using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Transaction;

namespace XBank.Domain.Models.InputModel
{
    public class TransactionInputModelCreate
    {
        public string Description { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public TransactionType TransactionType { get; set; }
    }
}
