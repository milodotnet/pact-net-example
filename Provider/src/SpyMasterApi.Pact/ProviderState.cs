namespace SpyMasterApi.Pact
{
    public class ProviderState
    {
        public string Consumer { get; set; }
        public string State { get; set; }

        public bool For(string consumerName)
        {
            return !string.IsNullOrEmpty(State) && Consumer == consumerName;
        }
    }
}