namespace SpyMasterApi.Pact
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Middleware.SpyMasterProviderState;
    using SpyMasterApi.Pact.Middleware.Pact;

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
            var providerStateSeeder = ConfigureProviderStates();
            app.UseMiddleware<SpyMasterProviderStateMiddleware>(providerStateSeeder);
            _apiStartup.Configure(app, env);
        }

        private static SpyMasterInMemoryProviderStateSeeder ConfigureProviderStates()
        {
            var providerStateSeeder = new SpyMasterInMemoryProviderStateSeeder();
            providerStateSeeder
                .AddProviderStateSetup(new ProviderState("SpyLens FrontEnd", "An agent '007' exists"),
                    service => service.Add(new AgentDetails("Roger", "Moore", new DateTime(1968, 03, 02), 80)));
            return providerStateSeeder;
        }
    }
}