using Microsoft.AspNetCore.Http;

namespace SpyMasterApi.Pact
{
    public interface IProviderStateSetup<in TDataProvider>
    {
        void SetupMatchingProviderState(TDataProvider dataProvider, HttpRequest request);
    }
}