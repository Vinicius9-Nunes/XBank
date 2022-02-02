using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;

namespace XBank.Domain.Interfaces.Core
{
    public interface ILocalRequestHttp<C>
    {
        Task<T> Get<T>(string baseUrl, string parameter) where T : C;
        Task<T> Put<T>(string baseUrl, object body, string parameter = null) where T : C;
        Task<T> PostQueryString<T>(string fullUrl) where T : C;
    }
}
