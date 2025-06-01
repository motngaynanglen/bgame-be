using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Transaction.Models
{
    public class WebhookData
    {
        public string orderCode { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string status { get; set; } // SUCCESS | CANCEL | FAIL
        public long createdAt { get; set; }
        public string signature { get; set; } // Checksum
    }
}
