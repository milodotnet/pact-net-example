using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PeopleStoreApi.Services;
using Xunit;
using Xunit.Abstractions;

namespace PeopleStoreApi.Pact
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public async Task Test1()
        {
            var baseAddress = $"http://localhost:8088";
            var webHost = WebHost
                .CreateDefaultBuilder()
                .UseKestrel()
                .UseStartup<TestStartup>()
                .UseUrls(baseAddress)
                .Build();
            await webHost
                .StartAsync();
            var c = new HttpClient();
            var pactVerifierConfig = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                }                                
            };
            IPactVerifier pactVerifier = new PactVerifier(pactVerifierConfig);
            
            pactVerifier                    
                .ProviderState($"{baseAddress}/provider-states")
                .ServiceProvider("PeopleStoreApi", baseAddress)
                .HonoursPactWith("CustomerFace Frontend")
                .PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Consumer{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}PeopleStore.Pacts{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}customerface_frontend-peoplestore_api.json")
                .Verify();
            await webHost.StopAsync();

        }
    }


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
                context.Response.Headers.Add("X-Xss-Protection", "1");
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");


                if (context.Request.Path.Value == "/provider-states")
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    if (context.Request.Method == HttpMethod.Post.ToString() &&
                        context.Request.Body != null)
                    {
                        string jsonRequestBody;
                        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                        {
                            jsonRequestBody = reader.ReadToEnd();
                        }

                        var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                        //A null or empty provider state key must be handled
                        if (providerState != null &&
                            !string.IsNullOrEmpty(providerState.State) &&
                            providerState.Consumer == ConsumerName)
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
        }
}
