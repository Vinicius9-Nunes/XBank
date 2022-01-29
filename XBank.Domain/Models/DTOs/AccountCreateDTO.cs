using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Account;

namespace XBank.Domain.Models.DTOs
{
    public class AccountCreateDTO : ModelDTO
    {
        public string HolderName { get; set; }
        public string HolderCpf { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
