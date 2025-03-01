using Data.Repository;
using Domain.Interfaces.Repository;
using Domain.Settings;
using Microsoft.Data.SqlClient;

namespace API.Configuration
{
    public static class DatabaseSetup
    {
        public static IServiceCollection AddSetupDatabase(this IServiceCollection services, AppSettingsConfig appSettings)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>(options =>
            {
                var connection = new SqlConnection(appSettings.ConnectionStrings.DefaultConnection);
                return new UnitOfWork(connection);
            });
            return services;
        }
    }
}
