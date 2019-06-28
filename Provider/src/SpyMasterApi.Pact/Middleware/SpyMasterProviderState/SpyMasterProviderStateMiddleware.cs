namespace SpyMasterApi.Pact.Middleware.SpyMasterProviderState
{
    using System;
    using HttpExtensions;
    using Microsoft.AspNetCore.Http;
    using Services;
    using SpyMasterApi.Pact.Middleware.Pact;

    public class SpyMasterProviderStateMiddleware : ProviderStateMiddleWare<IAgentsService>
    {
        private readonly SpyMasterInMemoryProviderStateSeeder _providerStateSeeder;

        public SpyMasterProviderStateMiddleware(RequestDelegate next) : base(next)
        {
            _providerStateSeeder = new SpyMasterInMemoryProviderStateSeeder();
            _providerStateSeeder = new SpyMasterProviderStateBuilder()
                .ForProviderState(new ProviderState("SpyLens FrontEnd", "An agent '007' exists"))
                .SeedData(service => service.Add(new AgentDetails("Roger", "Moore", new DateTime(1968, 03, 02), 80)))
                .Build();
        }

        protected override void MatchProviderState(IAgentsService agentsService, HttpRequest request)
        {
            if (!request.HasBody()) return;
            var providerState = request.GetBodyAsync<ProviderState>();
             _providerStateSeeder.MatchSeedingAction(providerState, agentsService as InMemoryAgentsService);
        }
    }
}