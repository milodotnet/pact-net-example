namespace SpyMasterApi.Pact.Middleware.SpyMasterProviderState
{
    using HttpExtensions;
    using Microsoft.AspNetCore.Http;
    using Services;
    using SpyMasterApi.Pact.Middleware.Pact;

    public class SpyMasterProviderStateMiddleware : ProviderStateMiddleWare<IAgentsService>
    {
        private readonly SpyMasterInMemoryProviderStateSeeder _providerStateSeeder;

        public SpyMasterProviderStateMiddleware(RequestDelegate next, SpyMasterInMemoryProviderStateSeeder providerStateSeeder) : base(next)
        {
            _providerStateSeeder = providerStateSeeder;
        }

        protected override void SetupMatchingProviderState(IAgentsService agentsService, HttpRequest request)
        {
            if (!request.HasBody()) return;
            var providerState = request.GetBodyAsync<ProviderState>();
             _providerStateSeeder.SetupProviderState(providerState, agentsService as InMemoryAgentsService);
        }
    }
}