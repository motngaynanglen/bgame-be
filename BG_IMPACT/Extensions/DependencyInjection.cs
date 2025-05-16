using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using BG_IMPACT.Services;
using System.Reflection;

namespace BG_IMPACT.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection DependencyInject(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductGroupRefRepository, ProductGroupRefRepository>();
            services.AddScoped<IBookListRepository, BookListRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IConsignmentOrderRepository, ConsignmentOrderRepository>();
            services.AddScoped<CloudinaryService>();

            return services;
        }
    }
}