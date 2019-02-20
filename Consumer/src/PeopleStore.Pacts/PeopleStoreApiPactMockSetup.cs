using System;
using System.IO;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;

namespace PeopleStore.Pacts
{
    public class PeopleStoreApiPactMockSetup : IDisposable
    {
        
        public IMockProviderService MockPeopleStoreService { get; }

        private static int MockServerPort => 9222;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";
        private IPactBuilder PactBuilder { get; }

        public PeopleStoreApiPactMockSetup()
        {
            PactBuilder = new PactBuilder(new PactConfig
                {
                    SpecificationVersion = "2.0.0",
                    LogDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}",
                    PactDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}"
                })
                .ServiceConsumer("CustomerFace FrontEnd")
                .HasPactWith("PeopleStore Api");

            MockPeopleStoreService = PactBuilder.MockService(MockServerPort, false, IPAddress.Any);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}