using System;
using System.Collections.Generic;

namespace SpyMasterApi.Pact
{
    public class SpyMasterInMemoryProviderStateSeeder
    {
        private readonly Dictionary<ProviderState, Action<InMemoryAgentsService>> _seedingActions = new Dictionary<ProviderState, Action<InMemoryAgentsService>>();

        public void AddProviderStateSetup(ProviderState state, Action<InMemoryAgentsService> seedingAction)
        {
            _seedingActions.Add(state, seedingAction);
        }

        public void SetupProviderState(ProviderState state, InMemoryAgentsService agentsService)
        {
            if (_seedingActions.ContainsKey(state))
            {
                _seedingActions[state].Invoke(agentsService);
            }
        }
    }
}