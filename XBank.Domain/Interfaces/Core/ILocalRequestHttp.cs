using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Domain.Interfaces.Core
{
    public interface ILocalRequestHttp
    {
        Task<T> Get<T>(string baseUrl, string parameter);
    }
}
