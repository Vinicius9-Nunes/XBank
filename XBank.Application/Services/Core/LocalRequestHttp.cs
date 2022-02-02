using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using XBank.Domain.Interfaces.Core;

namespace XBank.Application.Services.Core
{
    public class LocalRequestHttp<C> : ILocalRequestHttp<C>
    {
        public async Task<T> Get<T>(string baseUrl, string parameter) where T : C
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseRequest = await client.GetAsync(string.Concat(baseUrl, parameter));
            if (responseRequest.IsSuccessStatusCode)
            {
                string response = await responseRequest.Content.ReadAsStringAsync();
                try
                {
                    return JsonConvert.DeserializeObject<T>(response);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Não foi possivel converter para o tipo informado: {ex.Message}");
                }
            }
            else
                throw new HttpRequestException(await responseRequest.Content.ReadAsStringAsync());
        }

        public async Task<T> PostQueryString<T>(string fullUrl) where T : C
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseRequest = await client.PostAsJsonAsync(fullUrl, new { });
            if (responseRequest.IsSuccessStatusCode)
            {
                string response = await responseRequest.Content.ReadAsStringAsync();
                try
                {
                    return JsonConvert.DeserializeObject<T>(response);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Não foi possivel converter para o tipo informado: {ex.Message}");
                }
            }
            else
                throw new HttpRequestException(await responseRequest.Content.ReadAsStringAsync());
        }

        public async Task<T> Put<T>(string baseUrl, object body, string parameter = null) where T : C
        {
            HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string fullUrl = baseUrl;
            if (!string.IsNullOrEmpty(parameter))
                fullUrl += parameter;
            HttpResponseMessage responseRequest = await client.PutAsJsonAsync(fullUrl, body);
            if (responseRequest.IsSuccessStatusCode)
            {
                string response = await responseRequest.Content.ReadAsStringAsync();
                try
                {
                    return JsonConvert.DeserializeObject<T>(response);
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
