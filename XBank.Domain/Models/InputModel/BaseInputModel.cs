using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Models.InputModel
{
    public abstract class BaseInputModel
    {
        public abstract void RemoveCpfLetters();
    }
}
