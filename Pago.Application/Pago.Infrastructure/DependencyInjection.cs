using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pago.CrossCutting.Configs;
using Pago.Domain.Repositories;
using Pago.Domain.Services.WebServices;
using Pago.Infrastructure.Repositories.Base;
using Pago.Infrastructure.Services.WebServices;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pago.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(this IServiceCollection services, IConfiguration configInfo)
        {
            var appConfiguration = new AppConfiguration(configInfo);

            services.AddDbContext<PagoDbContext>(
                options => options.UseSqlServer(appConfiguration.ConexionDBPago)
                );

            services.AddRepositories(Assembly.GetExecutingAssembly());
            services.AddLogger(appConfiguration.LogMongoServerDB, appConfiguration.LogMongoDbCollection);

            var httpClientBuilder = services.AddHttpClient<IBancoService, BancoService>(
                options =>
                {
                    options.BaseAddress = new Uri(appConfiguration.UrlBaseServicioBanco);
                }
                );
        }
        public static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            var respositoryTypes = assembly
                .GetExportedTypes().Where(item => item.GetInterface(nameof(IRepository)) != null).ToList();


            foreach (var repositoryType in respositoryTypes)
            {
                var repositoryInterfaceType = repositoryType.GetInterfaces()
                    .Where(item => item.GetInterface(nameof(IRepository)) != null)
                    .First();

                services.AddScoped(repositoryInterfaceType, repositoryType);
            }
        }
        public static void AddLogger(this IServiceCollection services, string connectionStringDbLog, string collectionName)
        {
            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.MongoDB(connectionStringDbLog, collectionName: collectionName)
                .CreateLogger();


            services.AddLogging(builder =>
            {
                builder.AddSerilog(logger: serilogLogger, dispose: true);
            });
        }
    }
}
