using Pago.CrossCutting.Configs;

namespace Pago.API.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddHealthCheckConfiguration
            (this IServiceCollection services, IConfiguration configInfo)
        {
            var appConfiguration = new AppConfiguration(configInfo);

            services.AddHealthChecks()
                .AddSqlServer(connectionString: appConfiguration.ConexionDBPago);

            return services;
        }

        public static IApplicationBuilder UseHealthCheckConfiguration
           (this IApplicationBuilder services)
        {
            services.UseHealthChecks("/health");

            return services;
        }
    }
}
