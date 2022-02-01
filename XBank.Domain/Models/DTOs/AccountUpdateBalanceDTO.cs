using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Models.DTOs
{
    public class AccountUpdateBalanceDTO
    {
        public string HolderName { get; set; }
        public string HolderCpf { get; set; }
        public double Balance { get; set; }
    }
}
