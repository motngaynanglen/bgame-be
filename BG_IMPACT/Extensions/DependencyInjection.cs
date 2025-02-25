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
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductGroupRefRepository, ProductGroupRefRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
