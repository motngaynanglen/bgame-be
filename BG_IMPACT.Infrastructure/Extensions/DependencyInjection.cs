using BG_IMPACT.DTO.Models;
using BG_IMPACT.Infrastructure.Services;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using BG_IMPACT.Repository.Repositories.Implementations;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BG_IMPACT.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection DependencyInject(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            services.AddScoped<SqlConnection>(_ => new SqlConnection(connectionString));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductGroupRefRepository, ProductGroupRefRepository>();
            services.AddScoped<IProductTemplateRepository, ProductTemplateRepository>();
            services.AddScoped<IStoreTableRepository, StoreTableRepository>();
            services.AddScoped<IBookListRepository, BookListRepository>();
            services.AddScoped<IBookItemRepository, BookItemRepository>();
            services.AddScoped<IBookTableRepository, BookTableRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IConsignmentOrderRepository, ConsignmentOrderRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplyOrderRepository, SupplyOrderRepository>();
            services.AddScoped<IEmailServiceRepository, EmailServiceRepository>();
            services.AddScoped<ISupplyItemRepository, SupplyItemRepository>();

            services.AddScoped<CloudinaryService>();

            return services;
        }
    }
}