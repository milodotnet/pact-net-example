using System;
using System.Collections.Generic;
using SpyMasterApi.Services;

namespace SpyMasterApi.Pact
{
    public class ProviderStateSeederBuilder
    {
        private readonly Dictionary<ProviderState, Action<InMemoryAgentsService>> _seedingActions;

        public ProviderStateSeederBuilder()
        {
            _seedingActions = new Dictionary<ProviderState, Action<InMemoryAgentsService>>();
        }

        public ProviderStateSeederBuilder WithProviderState(ProviderState state, Action<InMemoryAgentsService> seedingAction)
        {
            _seedingActions.Add(state, seedingAction);
            return this;
        }

        public Dictionary<ProviderState, Action<InMemoryAgentsService>> Build()
        {
            return _seedingActions;
        }
    }
}