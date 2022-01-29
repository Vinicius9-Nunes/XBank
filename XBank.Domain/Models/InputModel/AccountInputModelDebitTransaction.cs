using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Models.InputModel
{
    public class AccountInputModelDebitTransaction
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public DateTime UpdateAt { get; set; }
    }
}
