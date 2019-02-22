using System;
using System.IO;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;

namespace SpyMaster.Pacts
{
    public class SpyMasterApiPactMockSetup : IDisposable
    {
        
        public IMockProviderService MockSpyMasterService { get; }

        private static int MockServerPort => 9222;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";
        private IPactBuilder PactBuilder { get; }

        public SpyMasterApiPactMockSetup()
        {
            PactBuilder = new PactBuilder(new PactConfig
                {
                    SpecificationVersion = "2.0.0",
                    LogDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}",
                    PactDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}"
                })
                .ServiceConsumer("SpyLens FrontEnd")
                .HasPactWith("SpyMaster Api");

            MockSpyMasterService = PactBuilder.MockService(MockServerPort, false, IPAddress.Any);
        }

        public void Dispose()
        {
            PactBuilder.Build();

            var pactPublisher = new PactPublisher("http://localhost");
            pactPublisher.PublishToBroker("..\\..\\..\\pacts\\SpyLens_frontend-SpyMaster_api.json","1.0.0", new[] { "master" });
        }
    }
}