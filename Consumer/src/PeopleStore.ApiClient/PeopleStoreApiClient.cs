using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PeopleStore.ApiClient
{
    public class PeopleStoreApiClient
    {
        private readonly HttpClient _client;

        public PeopleStoreApiClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<CustomerDetails> GetCustomerAsync(string customerId)
        {
            var request =new HttpRequestMessage(HttpMethod.Get, $"/customers/{customerId}");
            request.Headers.Add("Accept",  "application/json" );
            var response = await _client.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CustomerDetails>(responseJson);
        }
    }
}