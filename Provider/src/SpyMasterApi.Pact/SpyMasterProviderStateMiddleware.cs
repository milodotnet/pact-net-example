using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SpyMasterApi.Services;

namespace SpyMasterApi.Pact
{
    public class SpyMasterProviderStateMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ConsumerName = "SpyLens FrontEnd";
        private readonly IDictionary<string, Action<InMemoryAgentsService>> _providerStates;

        public SpyMasterProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action<InMemoryAgentsService>>
            {
                {
                    "An agent '007' exists",
                    service => service.Add(new AgentDetails("Roger", "Moore", new DateTime(1968,03,02),80))
                },
            };
        }
        public async Task InvokeAsync(HttpContext context, IAgentsService agentsService)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                if (context.Request.Method == HttpMethod.Post.ToString() && context.Request.Body != null)
                {
                    var providerState = ReadProviderStateFromContext(context);
                        
                    if (providerState != null && providerState.For(ConsumerName))
                    {
                        _providerStates[providerState.State].Invoke(agentsService as InMemoryAgentsService);
                    }
                    await context.Response.WriteAsync(string.Empty);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private static ProviderState ReadProviderStateFromContext(HttpContext context)
        {
            string jsonRequestBody;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                jsonRequestBody = reader.ReadToEnd();
            }

            var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);
            return providerState;
        }
    }
}