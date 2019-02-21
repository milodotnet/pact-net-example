using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleStoreApi.Services;

namespace PeopleStoreApi.Pact
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
            services.AddSingleton<ICustomerService, InMemoryCustomerService>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ProviderStateMiddleware>();
            _apiStartup.Configure(app, env);
            
        }
    }
}