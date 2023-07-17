using API_EF.Controllers;
using API_EF.Database;
using API_EF.Database.Repository;
using API_EF.Database.Repository.Contracts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API_EF_TEST.Helper
{
    public class ServiceProviderHelper
    {
        private class EmptyStartup
        {
            public EmptyStartup(IConfiguration _) { }
            public void ConfigureServices(IServiceCollection _) { }
            public void Configure(IApplicationBuilder _) { }
        }

        public static ServiceProvider GenerateServiceProvider()
        {
            //Startup startup = null;
            IServiceCollection serviceCollection = null;
            WebHost
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();
                    config.AddConfiguration(hostingContext.Configuration);
                    config.AddJsonFile("testsettings.json");
                    //startup = new Startup(config.Build());
                })
                .ConfigureServices(sc =>
                {
                    //startup.ConfigureServices(sc);
                    serviceCollection = sc;
                })
                .UseStartup<EmptyStartup>()
                .Build();

            //serviceCollection.AddDbContext<DBContext>(
            //    options => options.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=GD2015C1;TrustServerCertificate=True;Integrated Security=True;")
            //    //ServiceLifetime.Transient
            //    );
            serviceCollection.AddTransient<IUsuarioRepository, UsuarioRepository>();
            //serviceCollection.AddTransient<EmpleadoController>();
            //serviceCollection.AddTransient<ProductoController>();
            serviceCollection.AddTransient<UsuarioController>();

            var serviceProvider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions()
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });

            return serviceProvider;
        }
    }
}
