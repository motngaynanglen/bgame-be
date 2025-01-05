using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using System.Reflection;

namespace BG_IMPACT.Extensions
{
    public static class DependencyInjection 
    {
        public static IServiceCollection DependencyInject(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}
