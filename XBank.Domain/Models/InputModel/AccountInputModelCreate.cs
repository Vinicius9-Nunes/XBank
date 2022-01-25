using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Models.InputModel
{
    public class AccountInputModelCreate
    {
        [Required]
        public string HolderName { get; set; }
        [Required]
        public string HolderCpf { get; set; }
        [Required]
        public int DueDate { get; set; }

    }
}
