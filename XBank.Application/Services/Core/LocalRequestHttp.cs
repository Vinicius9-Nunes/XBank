using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XBank.Domain.Interfaces.Core;

namespace XBank.Application.Services.Core
{
    public class LocalRequestHttp : ILocalRequestHttp
    {
        public async Task<T> Get<T>(string baseUrl, string parameter)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseRequest = await client.GetAsync(string.Concat(baseUrl, parameter));
            if (responseRequest.IsSuccessStatusCode)
            {
                string response = await responseRequest.Content.ReadAsStringAsync();
                try
                {
                    return JsonSerializer.Deserialize<T>(response);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Não foi possivel converter para o tipo informado: {ex.Message}");
                }
            }
            else
                throw new HttpRequestException(await responseRequest.Content.ReadAsStringAsync());
        }
    }
}
