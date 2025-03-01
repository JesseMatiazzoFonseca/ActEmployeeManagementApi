namespace API.Configuration
{
    public static class DependenceInjectionSetup
    {
        public static IServiceCollection AddSetupDependenceInjection(this IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger<object>>();
            return services;
        }
    }
}
