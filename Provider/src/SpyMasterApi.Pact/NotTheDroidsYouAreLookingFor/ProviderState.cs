namespace SpyMasterApi.Pact
{
    public class ProviderState
    {
        public ProviderState(string consumer, string state)
        {
            Consumer = consumer;
            State = state;
        }

        public string Consumer { get; set; }
        public string State { get; set; }

        protected bool Equals(ProviderState other)
        {
            return string.Equals(Consumer, other.Consumer) && string.Equals(State, other.State);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProviderState) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Consumer != null ? Consumer.GetHashCode() : 0) * 397) ^ (State != null ? State.GetHashCode() : 0);
            }
        }
    }
}