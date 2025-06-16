using BG_IMPACT.DTO.Models;
using BG_IMPACT.Repository.Repositories.Implementations;
using BG_IMPACT.Repository.Repositories.Interfaces;

namespace BG_IMPACT.Config
{
    public static class EmailConfig
    {
        public static IServiceCollection EmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailServiceRepository, EmailServiceRepository>();

            return services;
        } 
    }
}
