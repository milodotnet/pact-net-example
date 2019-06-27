using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SpyMasterApi.Pact
{
    public class ProviderStateMiddleWareCompositional<TDataProvider>
    {
        public const string ProviderStatePath = "/provider-states";
        private readonly RequestDelegate _next;
        private readonly IProviderStateSetup<TDataProvider> _setup;

        protected ProviderStateMiddleWareCompositional(RequestDelegate next, IProviderStateSetup<TDataProvider> setup)
        {
            _next = next;
            _setup = setup;
        }

        public async Task InvokeAsync(HttpContext context, TDataProvider dataProvider)
        {
            if (IsProviderStateRequest(context) && context.IsPost())
            {
                _setup.SetupMatchingProviderState(dataProvider, context.Request);
                await context.OkResponse();
            }
            else
            {
                await _next(context);
            }
        }

        private static bool IsProviderStateRequest(HttpContext context)
        {
            return context.Request.Path.Value == ProviderStatePath;
        }
    }
}