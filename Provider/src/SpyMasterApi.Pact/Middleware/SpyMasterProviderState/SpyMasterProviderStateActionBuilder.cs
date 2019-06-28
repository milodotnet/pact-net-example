namespace SpyMasterApi.Pact.Middleware.SpyMasterProviderState
{
    using System;
    using Pact;

    public class SpyMasterProviderStateActionBuilder
    {
        private readonly SpyMasterProviderStateBuilder _spyMasterProviderStateBuilder;
        private readonly SpyMasterInMemoryProviderStateSeeder _spyMasterInMemoryProviderStateSeeder;
        private readonly ProviderState _state;

        public SpyMasterProviderStateActionBuilder(SpyMasterProviderStateBuilder spyMasterProviderStateBuilder,
            SpyMasterInMemoryProviderStateSeeder spyMasterInMemoryProviderStateSeeder,
            ProviderState state)
        {
            _spyMasterProviderStateBuilder = spyMasterProviderStateBuilder;
            _spyMasterInMemoryProviderStateSeeder = spyMasterInMemoryProviderStateSeeder;
            _state = state;
        }

        public SpyMasterProviderStateBuilder SeedData(Action<InMemoryAgentsService> action)
        {
            _spyMasterInMemoryProviderStateSeeder.AddProviderStateSetup(_state, action);
            return _spyMasterProviderStateBuilder;
        }
    }
}