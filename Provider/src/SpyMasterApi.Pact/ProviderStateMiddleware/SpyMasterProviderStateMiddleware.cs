using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SpyMasterApi.Services;

namespace SpyMasterApi.Pact
{
    public abstract class ProviderStateMiddleWare<TDataProvider>
    {
        public const string ProviderStatePath = "/provider-states";
        private readonly RequestDelegate _next;

        protected ProviderStateMiddleWare(RequestDelegate next)
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
            return context.Request.Path.Value.EndsWith(ProviderStatePath);
        }


    }
    public class SpyMasterProviderStateMiddleware : ProviderStateMiddleWare<IAgentsService>
    {
        private readonly SpyMasterInMemoryProviderStateSeeder _providerStateSeeder;

        public SpyMasterProviderStateMiddleware(RequestDelegate next, SpyMasterInMemoryProviderStateSeeder providerStateSeeder) : base(next)
        {
            _providerStateSeeder = providerStateSeeder;
        }

        protected override void SetupMatchingProviderState(IAgentsService agentsService, HttpRequest request)
        {
            if (!request.HasBody()) return;
            var providerState = request.GetBodyAsync<ProviderState>();
             _providerStateSeeder.SetupProviderState(providerState, agentsService as InMemoryAgentsService);
        }
    }
}