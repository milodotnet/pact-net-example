using Microsoft.AspNetCore.Http;
using SpyMasterApi.Services;

namespace SpyMasterApi.Pact
{
    public class SpyMasterProviderStateSetup : IProviderStateSetup<IAgentsService>
    {
        private readonly SpyMasterInMemoryProviderStateSeeder _providerStateSeeder;

        public SpyMasterProviderStateSetup(SpyMasterInMemoryProviderStateSeeder providerStateSeeder) 
        {
            _providerStateSeeder = providerStateSeeder;
        }
        public void SetupMatchingProviderState(IAgentsService dataProvider, HttpRequest request)
        {
            if (!request.HasBody()) return;
            var providerState = request.GetBodyAsync<ProviderState>();
            _providerStateSeeder.SetupProviderState(providerState, dataProvider as InMemoryAgentsService);
        }
    }
}