using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Models.DTOs
{
    public class AccountUpdateDTO : ModelDTO
    {
        public string HolderName { get; set; }
        public int DueDate { get; set; }
        public string HolderCpf { get; set; }
    }
}
