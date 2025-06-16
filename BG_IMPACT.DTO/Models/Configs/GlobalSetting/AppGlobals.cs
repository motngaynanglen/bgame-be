using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.DTO.Models.Configs.GlobalSetting
{
    public static class AppGlobals
    {
        public static string ConnectionString { get; set; } = string.Empty;
        public static string Username {  get; set; } = string.Empty;
        public static string Password { get; set; } = string.Empty;
        public static Guid ID { get; set; }
    }
}
