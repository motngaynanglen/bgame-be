﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.DTO.Models.Configs.GlobalSetting
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; } 
        public string SmtpUsername { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
    }
}
