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
using SpyMaster.ApiClient;

namespace SpyMaster.Pacts
{
    public class SpyMasterApiClientShould : IClassFixture<SpyMasterApiPactMockSetup>
    {

        private readonly IMockProviderService _mockSpyMaster;
        private readonly string _mockSpyMasterBaseUri;

        public SpyMasterApiClientShould(SpyMasterApiPactMockSetup mockServerSetup)
        {
            _mockSpyMaster = mockServerSetup.MockSpyMasterService;
            _mockSpyMasterBaseUri = mockServerSetup.MockProviderServiceBaseUri;
            _mockSpyMaster.ClearInteractions();
        }

        [Fact]
        public async Task GetAgentDetails()
        {
            
            _mockSpyMaster.Given("An agent '007' exists")
                .UponReceiving("a request to retrieve agent '007'")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/agents/007",
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
                        dateOfBirth= Match.Regex("1968-03-02T00:00:00Z", DateFormats.ValidIso8601Date) , 
                        age = Match.Type(50),
                    }
                });

            var httpClient = new HttpClient {BaseAddress = new Uri(_mockSpyMasterBaseUri)};
            var spyMasterApiClient = new SpyMasterApiClient(httpClient);

            var expectedAgentDetails = new AgentDetails
            {
                Name = "James",
                Surname = "Bond",
                DateOfBirth = new DateTime(1968, 03, 02),
                Age = 50
            };

            var result = await spyMasterApiClient.GetAgentAsync("007");
          
            result.Should().BeEquivalentTo(expectedAgentDetails);
            _mockSpyMaster.VerifyInteractions();
        }
    }
}
