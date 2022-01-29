using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Models.DTOs
{
    public class UpdateAccountDebitDTO
    {
        public long Id { get; set; }
        public string HolderName { get; set; }
        public double Balance { get; set; }
        public int DueDate { get; set; }
    }
}
