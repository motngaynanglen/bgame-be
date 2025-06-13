using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.DTO.Models
{
    public class MessageData {
        public string StatusCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
    public static class MessageCode
    {
        public static Dictionary<string, MessageData> Messages { get; set; } = new();

        public static void Initialize()
        {
            Messages.Add("100", new MessageData { StatusCode = "200", Message = "Lấy sản phẩm thành công." });
        }

        public static MessageData Get(string code)
        {
            return Messages[code] ?? new MessageData { StatusCode = "404", Message = "Mã lỗi không tồn tại." };
        }
    }
}
