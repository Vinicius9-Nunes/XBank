using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Models.InputModel
{
    public class AccountInputModelUpdate
    {
        public string HolderName { get; set; }

        public int DueDate { get; set; }
    }
}
