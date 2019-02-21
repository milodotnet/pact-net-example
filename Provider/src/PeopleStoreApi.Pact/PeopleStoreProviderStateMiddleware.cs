using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PeopleStoreApi.Services;

namespace PeopleStoreApi.Pact
{
    public class PeopleStoreProviderStateMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ConsumerName = "CustomerFace FrontEnd";
        private readonly IDictionary<string, Action<InMemoryCustomerService>> _providerStates;

        public PeopleStoreProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action<InMemoryCustomerService>>
            {
                {
                    "Customer '007' exists",
                    service => service.Add(new Customer("James", "Bond", new DateTime(1968,03,02),50))
                },
            };
        }
        public async Task InvokeAsync(HttpContext context, ICustomerService customerService)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                if (context.Request.Method == HttpMethod.Post.ToString() && context.Request.Body != null)
                {
                    var providerState = ReadProviderStateFromContext(context);
                        
                    if (providerState != null && providerState.For(ConsumerName))
                    {
                        _providerStates[providerState.State].Invoke(customerService as InMemoryCustomerService);
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