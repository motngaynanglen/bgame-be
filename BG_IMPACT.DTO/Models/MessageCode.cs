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

        //Mỗi loại data đều phải phân loại ra 4 loại trạng thái cơ bản (200, 404, 403, 422, thêm vào nếu có)
        public static void Initialize()
        {
            AccountInitialize();
            ProductInitialize();
            OrderInitialize();
            BookListInitialize();
        }

        private static void AccountInitialize()
        {
            //StatusCode - 200
            AddMessage("XXX", "200", "XXX");

            //StatusCode - 403
            AddMessage("XXX", "403", "XXX");

            //StatusCode - 422
            AddMessage("XXX", "422", "XXX");

            //StatusCode - 404
            AddMessage("XXX", "404", "XXX");
        }

        private static void ProductInitialize()
        {
            //StatusCode - 200
            AddMessage("XXX", "200", "XXX");

            //StatusCode - 403
            AddMessage("XXX", "403", "XXX");

            //StatusCode - 422
            AddMessage("XXX", "422", "XXX");

            //StatusCode - 404
            AddMessage("XXX", "404", "XXX");
        }

        private static void OrderInitialize()
        {
            //StatusCode - 200
            AddMessage("XXX", "200", "XXX");

            //StatusCode - 403
            AddMessage("XXX", "403", "XXX");

            //StatusCode - 422
            AddMessage("XXX", "422", "XXX");

            //StatusCode - 404
            AddMessage("XXX", "404", "XXX");
        }

        private static void BookListInitialize()
        {
            //StatusCode - 200
            AddMessage("XXX", "200", "XXX");

            //StatusCode - 403
            AddMessage("XXX", "403", "XXX");

            //StatusCode - 422
            AddMessage("XXX", "422", "XXX");

            //StatusCode - 404
            AddMessage("XXX", "404", "XXX");
        }

        private static void AddMessage(string key, string status, string message)
        {
            Messages.Add(key, new MessageData { StatusCode = status, Message = message });
        }

        public static MessageData Get(string code)
        {
            return Messages[code] ?? new MessageData { StatusCode = "404", Message = "Mã lỗi không tồn tại." };
        }
    }
}
