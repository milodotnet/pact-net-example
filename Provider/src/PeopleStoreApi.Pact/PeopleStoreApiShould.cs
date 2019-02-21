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
                }                                
            };
            IPactVerifier pactVerifier = new PactVerifier(pactVerifierConfig);
            
            pactVerifier                    
                .ProviderState($"{baseAddress}/provider-states")
                .ServiceProvider("PeopleStoreApi", baseAddress)
                .HonoursPactWith("CustomerFace Frontend")
                .PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Consumer{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}PeopleStore.Pacts{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}customerface_frontend-peoplestore_api.json")
                .Verify();
            await webHost.StopAsync();

        }
    }
}
