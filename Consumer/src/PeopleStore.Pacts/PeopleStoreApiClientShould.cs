using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using PactNet;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using PeopleStore.ApiClient;

namespace PeopleStore.Pacts
{
    public class PeopleStoreApiClientShould : IClassFixture<PeopleStoreApiPactMockSetup>
    {

        private readonly IMockProviderService _mockPeopleStore;
        private readonly string _mockPeopleStoreBaseUri;

        public PeopleStoreApiClientShould(PeopleStoreApiPactMockSetup mockServerSetup)
        {
            _mockPeopleStore = mockServerSetup.MockPeopleStoreService;
            _mockPeopleStoreBaseUri = mockServerSetup.MockProviderServiceBaseUri;
            _mockPeopleStore.ClearInteractions();
        }

        [Fact]
        public async Task GetACustomersDetails()
        {
            var validISO8601Date = @"^(-?(?:[1-9][0-9]*)?[0-9]{4})-(1[0-2]|0[1-9])-(3[0-1]|0[1-9]|[1-2][0-9])T(2[0-3]|[0-1][0-9]):([0-5][0-9]):([0-5][0-9])(\.[0-9]+)?(Z|[+-](?:2[0-3]|[0-1][0-9]):[0-5][0-9])?$";
            _mockPeopleStore.Given("Customer '007' exists")
                .UponReceiving("a request to retrieve customer '007'")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/customers/007",
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" },
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        name = Match.Type("James"),
                        surname= Match.Type("Bond"),
                        dateOfBirth= Match.Regex("1968-03-02T00:00:00Z", validISO8601Date) , 
                        age = Match.Type(50),
                    }
                });

            var httpClient = new HttpClient {BaseAddress = new Uri(_mockPeopleStoreBaseUri)};
            var consumer = new PeopleStoreApiClient(httpClient);

            var expectedCustomerDetails = new CustomerDetails
            {
                Name = "James",
                Surname = "Bond",
                DateOfBirth = new DateTime(1968, 03, 02),
                Age = 50
            };

            var result = await consumer.GetCustomerAsync("007");

          
            result.Should().BeEquivalentTo(expectedCustomerDetails);
            _mockPeopleStore.VerifyInteractions();
        }
    }
}
