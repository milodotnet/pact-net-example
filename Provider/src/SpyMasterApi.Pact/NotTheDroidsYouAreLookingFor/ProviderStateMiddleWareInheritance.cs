using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SpyMasterApi.Pact
{
    public abstract class ProviderStateMiddleWareInheritance<TDataProvider>
    {
        public const string ProviderStatePath = "/provider-states";
        private readonly RequestDelegate _next;

        protected ProviderStateMiddleWareInheritance(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TDataProvider dataProvider)
        {
            if (IsProviderStateRequest(context) && context.IsPost())
            {
                SetupMatchingProviderState(dataProvider, context.Request);
                await context.OkResponse();
            }
            else
            {
                await _next(context);
            }
        }

        protected abstract void SetupMatchingProviderState(TDataProvider dataProvider, HttpRequest request);

        private static bool IsProviderStateRequest(HttpContext context)
        {
            return context.Request.Path.Value == ProviderStatePath;
        }
    }
}