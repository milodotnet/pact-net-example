namespace SpyMasterApi.Pact.Middleware.SpyMasterProviderState
{
    using Pact;

    public class SpyMasterProviderStateBuilder
    {
        private readonly SpyMasterInMemoryProviderStateSeeder _spyMasterInMemoryProviderStateSeeder;

        public SpyMasterProviderStateBuilder()
        {
            _spyMasterInMemoryProviderStateSeeder = new SpyMasterInMemoryProviderStateSeeder();
        }
        public SpyMasterProviderStateActionBuilder ForProviderState(ProviderState state)
        {
            return new SpyMasterProviderStateActionBuilder(this, _spyMasterInMemoryProviderStateSeeder, state);
        }

        public SpyMasterInMemoryProviderStateSeeder Build()
        {
            return _spyMasterInMemoryProviderStateSeeder;
        }
    }
}