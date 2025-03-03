using API.Validators;
using Microsoft.AspNetCore.Mvc;
using Shared.Validators;

namespace API.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddActionFilter(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ValidatorActionFilter>();
            });
        }
    }
}
