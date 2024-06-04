using SudLife_Premiumcalculation.APILayer.API.Service.Interface;
using System.Text.Json;
using System.Text;

namespace SudLife_Premiumcalculation.APILayer.API.Service.Services
{
    //Left-Dispose
    public class ConsumeApiService : IConsumeApiService
    {
        private readonly HttpClient _httpClient;
        public ConsumeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> ConsumeAPI<T>(HttpMethod method, string url, object data = null, IDictionary<string, string> headers = null)
        {
            var requestMessage = new HttpRequestMessage(method, url);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }
            if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                requestMessage.Content = jsonContent;
            }
            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();


            return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
