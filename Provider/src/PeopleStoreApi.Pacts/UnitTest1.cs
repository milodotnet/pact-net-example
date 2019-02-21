using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Newtonsoft.Json;
// using PactNet;
// using PeopleStoreApi.Services;
using Xunit;
// using Microsoft.AspNetCore.TestHost;

namespace PeopleStoreApi.Pacts
{
//     public class ProviderState
//     {
//         public string Consumer { get; set; }
//         public string State { get; set; }
//     }
//     public class ProviderStateMiddleware
//     {
//         private readonly RequestDelegate _next;
//         private const string ConsumerName = "Event API Consumer";
//         private readonly Func<IDictionary<string, object>, Task> m_next;
//         private readonly IDictionary<string, Action> _providerStates;

//         public ProviderStateMiddleware(RequestDelegate next)
//         {
//             _next = next;

// //            _providerStates = new Dictionary<string, Action>
// //            {
// //                {
// //                    "there are events with ids '45D80D13-D5A2-48D7-8353-CBB4C0EAABF5', '83F9262F-28F1-4703-AB1A-8CFD9E8249C9' and '3E83A96B-2A0C-49B1-9959-26DF23F83AEB'",
// //                    InsertEventsIntoDatabase
// //                },
// //                {
// //                    "there is an event with id '83f9262f-28f1-4703-ab1a-8cfd9e8249c9'",
// //                    InsertEventIntoDatabase
// //                },
// //                {
// //                    "there is one event with type 'DetailsView'",
// //                    EnsureOneDetailsViewEventExists
// //                }
// //            };
//         }
//         public async Task Invoke(HttpContext context)
//         {
//             context.Response.Headers.Add("X-Xss-Protection", "1");
//             context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
//             context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            
            
//             if (context.Request.Path.Value == "/provider-states")
//             {
//                 context.Response.StatusCode = (int)HttpStatusCode.OK;

//                 if (context.Request.Method == HttpMethod.Post.ToString() &&
//                     context.Request.Body != null)
//                 {
//                     string jsonRequestBody;
//                     using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
//                     {
//                         jsonRequestBody = reader.ReadToEnd();
//                     }

//                     var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

//                     //A null or empty provider state key must be handled
//                     if (providerState != null &&
//                         !string.IsNullOrEmpty(providerState.State) &&
//                         providerState.Consumer == ConsumerName)
//                     {
//                         _providerStates[providerState.State].Invoke();
//                     }

//                     await context.Response.WriteAsync(string.Empty);
//                 }
//             }
//             else
//             {
//                 await _next(context);
//             }
//         }
//     }

//     public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
//     {
//         protected override void ConfigureWebHost(IWebHostBuilder builder)
//         {
//             builder.ConfigureServices(services =>
//             {
//                 services.AddScoped<ICustomerService, InMemoryCustomerService>();
//             });            
//         }
//     }
//     public class TestStartup
//     {
//         private readonly Startup _apiStartup;

//         public TestStartup(IConfiguration configuration)
//         {
//             _apiStartup = new Startup(configuration);
//         }
//         public void ConfigureServices(IServiceCollection services)
//         {
//             _apiStartup.ConfigureServices(services);
//         }
//         public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//         {
//             _apiStartup.Configure(app, env);
//             app.UseMiddleware<ProviderStateMiddleware>();               
//         }
//     }
//     public class InMemoryCustomerService : ICustomerService
//     {

//     }

    public class UnitTest1 //:  IClassFixture<CustomWebApplicationFactory<Startup>>
    {        
        //private HttpClient _client;
        
        public UnitTest1()
        {
          //  _client = factory.CreateClient();            
        }
        [Fact]
        public void Test1()
        {
            //const string serviceUri = "http://localhost:9222";
//            var config = new PactVerifierConfig
//            {
//                Outputters = new List<IOutput>
//                {
//                    new PactNet.Infrastructure.Outputters.ConsoleOutput()
//                }
//            };
            Assert.True(true);
            
                //Act / Assert
            // IPactVerifier pactVerifier = new PactVerifier(new PactVerifierConfig());
            // pactVerifier                    
            //     .ProviderState($"{_client.BaseAddress}/provider-states")
            //     .ServiceProvider("PeopleStoreApi", _client.BaseAddress.ToString())
            //     .HonoursPactWith("CustomerFace Frontend")
            //     .PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Consumer{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}PeopleStore.Pacts{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}customerface_frontend-peoplestore_api.json")
            //     .Verify();
            
        }
    }
}
