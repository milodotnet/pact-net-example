using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpyMasterApi.Services;

namespace SpyMasterApi.Pact
{
    public class TestStartup
    {
        private readonly Startup _apiStartup;

        public TestStartup(IConfiguration configuration)
        {
            _apiStartup = new Startup(configuration);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            _apiStartup.ConfigureServices(services);
            services.AddSingleton<IAgentsService, InMemoryAgentsService>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var providerStates = SetupProviderStates();

            app.UseMiddleware<SpyMasterProviderStateMiddleware>(providerStates);
            _apiStartup.Configure(app, env);
            
        }

        private static Dictionary<ProviderState, Action<InMemoryAgentsService>> SetupProviderStates()
        {
            var providerStateSeederBuilder = new ProviderStateSeederBuilder();

            providerStateSeederBuilder = providerStateSeederBuilder
                .WithProviderState(new ProviderState("SpyLens FrontEnd", "An agent '007' exists"),
                    service => service.Add(new AgentDetails("Roger", "Moore", new DateTime(1968, 03, 02), 80)));

            return providerStateSeederBuilder.Build();
        }
    }
}