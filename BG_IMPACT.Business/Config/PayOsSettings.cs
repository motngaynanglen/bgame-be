﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Config
{
    public class PayOsSettings
    {
        public required string ClientId { get; set; }
        public required string ApiKey { get; set; }
        public required string ChecksumKey { get; set; }
        public required string ReturnUrl { get; set; }
        public required string CancelUrl { get; set; }
    }
}
