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
            services.AddSingleton<SpyMasterInMemoryProviderStateSeeder, SpyMasterInMemoryProviderStateSeeder>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<SpyMasterProviderStateMiddleware>();
            _apiStartup.Configure(app, env);
        }
    }
}