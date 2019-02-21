using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace PeopleStoreApi.Pact
{
    public class PeopleStoreApiShould
    {
        private readonly ITestOutputHelper _output;

        public PeopleStoreApiShould(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public async Task HonourPactWithCustomerFace()
        {
            var baseAddress = $"http://localhost:8088";
            var webHost = WebHost
                .CreateDefaultBuilder()
                .UseKestrel()
                .UseStartup<TestStartup>()
                .UseUrls(baseAddress)
                .Build();

            await webHost.StartAsync();

            var pactVerifierConfig = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                }  ,
                ProviderVersion = "1.0.0", //NOTE: This is required for this feature to work
                PublishVerificationResults = !string.IsNullOrEmpty("1.0.0")
            };
            IPactVerifier pactVerifier = new PactVerifier(pactVerifierConfig);
            
            pactVerifier                    
                .ProviderState($"{baseAddress}/provider-states")
                .ServiceProvider("PeopleStoreApi", baseAddress)
                .HonoursPactWith("CustomerFace Frontend")
                .PactUri($"http://localhost/pacts/provider/PeopleStore%20Api/consumer/CustomerFace%20FrontEnd/latest")
                .Verify();
            await webHost.StopAsync();

        }
    }
}
