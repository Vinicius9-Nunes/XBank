using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; private set; }
        public DateTime CreatAt { get; private set; }
        public DateTime UpdateAt { get; private set; }
    }
}
