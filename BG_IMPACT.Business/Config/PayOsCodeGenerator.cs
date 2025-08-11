using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Config
{
    public class PayOsCodeGenerator
    {
        private static long _lastTimestamp = 0;
        private static int _counter = 0;
        private static readonly object _lock = new object();
        public long GenerateCode()
        {
            lock (_lock) // Lock để đảm bảo thread-safe
            {
                long timestamp = long.Parse(DateTime.UtcNow.ToString("yyMMddHHmmss")); // chỉ cần 2 số cuối của năm 2025 là được, đủ unique rồi.
                // có thể chuyển qua tick nếu thấy chậm quá :v
                if (timestamp == _lastTimestamp)
                {
                    // Nếu vẫn trong cùng 1 giây, tăng bộ đếm lên
                    _counter++;
                }
                else
                {
                    // Nếu sang giây mới, reset bộ đếm
                    _lastTimestamp = timestamp;
                    _counter = 0;
                }

                if (_counter >= 1000)
                {
                    // Xử lý trường hợp có hơn 1000 order trong 1 giây (rất hiếm) :v rất vô lí
                    throw new InvalidOperationException("Code Generator đã tới giới hạn 1000");
                    // Sau này có thể làm cái nâng cấp kiểu: delay qua giây tiếp theo mà gen code sau.
                }

                // Ghép timestamp và bộ đếm bằng toán học
                // Dịch chuyển timestamp sang trái 4 chữ số và cộng với bộ đếm
                return (timestamp * 10000) + _counter;
            }
        }
    }
}
