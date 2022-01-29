using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Account;

namespace XBank.Domain.Models.DTOs
{
    public class AccountDTO
    {
        public string HolderName { get; set; }
        public string HolderCpf { get; set; }
        public double Balance { get; set; }
        public int DueDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public DateTime CreatAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
