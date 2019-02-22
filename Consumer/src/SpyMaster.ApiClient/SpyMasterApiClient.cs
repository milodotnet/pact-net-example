using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpyMaster.ApiClient
{
    public class SpyMasterApiClient
    {
        private readonly HttpClient _client;

        public SpyMasterApiClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<AgentDetails> GetAgentAsync(string customerId)
        {
            var request =new HttpRequestMessage(HttpMethod.Get, $"/agents/{customerId}");
            request.Headers.Add("Accept",  "application/json" );
            var response = await _client.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AgentDetails>(responseJson);
        }
    }
}