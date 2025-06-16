using BG_IMPACT.DTO.Models.Configs.GlobalSetting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Infrastructure.Extensions
{
    public static class AppGlobalsConfig
    {
        public static void Initialize(IConfiguration configuration)
        {
            AppGlobals.Username = configuration["Admin:Username"] ?? string.Empty;
            AppGlobals.Password = configuration["Admin:Password"] ?? string.Empty;
            AppGlobals.ID = Guid.Parse(configuration["Admin:ID"] ?? string.Empty);
        }
    }
}
