namespace SpyMasterApi.Pact.Middleware.Pact
{
    using Microsoft.AspNetCore.Http;

    public interface IProviderStateSetup<in TDataProvider>
    {
        void SetupMatchingProviderState(TDataProvider dataProvider, HttpRequest request);
    }
}