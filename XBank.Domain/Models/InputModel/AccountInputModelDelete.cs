using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Account;

namespace XBank.Domain.Models.InputModel
{
    public class AccountInputModelDelete
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public AccountStatus AccountStatus { get; set; }
    }
}
