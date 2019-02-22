using SpyMasterApi.Services;

namespace SpyMasterApi.Pact
{
    public class InMemoryAgentsService : IAgentsService
    {
        private AgentDetails _agentDetails;

        public void Add(AgentDetails agentDetails)
        {
            _agentDetails = agentDetails;
        }

        public AgentDetails Get(string id)
        {
            return _agentDetails;
        }
    }
}