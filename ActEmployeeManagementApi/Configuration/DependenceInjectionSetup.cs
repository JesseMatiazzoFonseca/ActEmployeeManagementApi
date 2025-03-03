using Core.Services;
using Data.Helpers.InitialDataBase;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Settings;

namespace API.Configuration
{
    public static class DependenceInjectionSetup
    {
        public static IServiceCollection AddSetupDependenceInjection(this IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger<object>>();
            services.AddSingleton<InitialDataBase>();
            services.AddScoped<AppSettingsConfig>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();

            return services;
        }
    }
}
