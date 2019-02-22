using SpyMasterApi.Services;

namespace SpyMasterApi.Pact
{
    public class InMemoryCustomerService : ICustomerService
    {
        private Customer _customer;

        public void Add(Customer customer)
        {
            _customer = customer;
        }

        public Customer Get(string id)
        {
            return _customer;
        }
    }
}