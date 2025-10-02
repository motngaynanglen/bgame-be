using BG_IMPACT.DTO.Models.Configs.GlobalSetting;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace BG_IMPACT.Repository.Repositories.Implementations
{
    public class EmailServiceRepository : IEmailServiceRepository
    {
        private readonly EmailSettings _emailSettings;

        public EmailServiceRepository(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SmtpUsername, _emailSettings.FromName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendBookListSuccessfullyAsync(string email, object data, string total_price)
        {
            // Kiểm tra dữ liệu đầu vào
            if (data == null) return;

            // Ép kiểu về list item
            var itemList = ((IEnumerable<dynamic>)data).ToList();
            if (itemList.Count == 0) return;

            string subject = "[BG Impact] Thông tin đơn nhập hàng mới";

            var htmlBuilder = new StringBuilder();

            // Tiêu đề mail
            htmlBuilder.Append("<h2>Danh sách sản phẩm trong đơn hàng</h2>");
            htmlBuilder.AppendFormat("<p>Tổng giá trị đơn hàng: <b>{0} VND</b>.</p>", total_price);
            htmlBuilder.Append("<br/>");

            // Bảng chi tiết
            htmlBuilder.Append("<table border='1' cellpadding='6' cellspacing='0' style='border-collapse: collapse; width:100%;'>");
            htmlBuilder.Append("<tr style='background-color:#f2f2f2;'><th>STT</th><th>Tên sản phẩm</th><th>Số lượng</th></tr>");

            foreach (var item in itemList)
            {
                htmlBuilder.AppendFormat(
                    "<tr><td style='text-align:center;'>{0}</td><td>{1}</td><td style='text-align:center;'>{2}</td></tr>",
                    item.Index,
                    item.name,
                    item.quantity
                );
            }

            htmlBuilder.Append("</table>");
            htmlBuilder.Append("<br/>");

            // Chữ ký công ty
            htmlBuilder.Append("<p>Trân trọng,</p>");
            htmlBuilder.Append("<p><b>BG Impact</b></p>");
            htmlBuilder.Append("<p>Hotline: 0123 456 789</p>");
            htmlBuilder.Append("<p>Email: support@bgimpact.com</p>");

            // Gửi mail
            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                EnableSsl = true
            };

            await SendEmailAsync(email, subject, htmlBuilder.ToString());
        }

        public async Task SendOrderSuccessfullyAsync(string email, string code, List<OrderItemModel> items)
        {
            string subject = "[BG Impact] Xác nhận đơn hàng #" + code;

            var htmlBuilder = new StringBuilder();

            // Tiêu đề mail
            htmlBuilder.Append("<h2>Cảm ơn bạn đã mua sắm tại BG Impact!</h2>");
            htmlBuilder.AppendFormat("<p>Xin chào,</p>");
            htmlBuilder.AppendFormat("<p>Đơn hàng của bạn với mã <b>{0}</b> đã được đặt thành công.</p>", code);
            htmlBuilder.AppendFormat("<p>Tổng giá trị đơn hàng: <b>{0:N0} VND</b>.</p>", items.Sum(x => x.current_price));
            htmlBuilder.Append("<p>Chúng tôi sẽ liên hệ để xác nhận và giao hàng trong thời gian sớm nhất.</p>");
            htmlBuilder.Append("<br/>");

            // Bảng danh sách sản phẩm
            htmlBuilder.Append("<h3>Chi tiết đơn hàng</h3>");
            htmlBuilder.Append("<table border='1' cellpadding='6' cellspacing='0' style='border-collapse: collapse; width:100%;'>");
            htmlBuilder.Append("<tr style='background-color:#f2f2f2;'><th>STT</th><th>Tên sản phẩm</th><th>Giá (VND)</th><th>Số lượng</th></tr>");

            // Gom nhóm theo tên sản phẩm
            var groupedItems = items
                .GroupBy(x => new { x.template_product_name, x.current_price })
                .Select(g => new
                {
                    ProductName = g.Key.template_product_name,
                    Price = g.Key.current_price,
                    Quantity = g.Count()
                })
                .ToList();

            int index = 1;
            foreach (var item in groupedItems)
            {
                htmlBuilder.AppendFormat(
                    "<tr><td style='text-align:center;'>{0}</td><td>{1}</td><td style='text-align:right;'>{2:N0}</td><td style='text-align:center;'>{3}</td></tr>",
                    index++,
                    item.ProductName,
                    item.Price,
                    item.Quantity
                );
            }
            htmlBuilder.Append("</table>");
            htmlBuilder.Append("<br/>");

            // Chữ ký công ty
            htmlBuilder.Append("<p>Trân trọng,</p>");
            htmlBuilder.Append("<p><b>BG Impact</b></p>");
            htmlBuilder.Append("<p>Hotline: 0123 456 789</p>");
            htmlBuilder.Append("<p>Email: support@bgimpact.com</p>");

            // Gửi email
            await SendEmailAsync(email, subject, htmlBuilder.ToString());
        }

        public async Task SendOrderCancelledAsync(string email, string code)
        {
            string subject = "[BG Impact] Đơn hàng #" + code + " đã bị hủy";

            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<h2>Thông báo hủy đơn hàng</h2>");
            htmlBuilder.AppendFormat("<p>Xin chào,</p>");
            htmlBuilder.AppendFormat("<p>Đơn hàng của bạn với mã <b>{0}</b> đã được hủy theo yêu cầu hoặc do có sự cố trong quá trình xử lý.</p>", code);
            htmlBuilder.Append("<p>Nếu có thắc mắc, vui lòng liên hệ bộ phận chăm sóc khách hàng.</p>");
            htmlBuilder.Append("<br/>");

            htmlBuilder.Append("<p>Trân trọng,</p>");
            htmlBuilder.Append("<p><b>BG Impact</b></p>");
            htmlBuilder.Append("<p>Hotline: 0123 456 789</p>");
            htmlBuilder.Append("<p>Email: support@bgimpact.com</p>");

            await SendEmailAsync(email, subject, htmlBuilder.ToString());
        }

        public async Task SendOrderPaidAsync(string email, string code)
        {
            string subject = "[BG Impact] Thanh toán thành công đơn hàng #" + code;

            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<h2>Xác nhận thanh toán</h2>");
            htmlBuilder.AppendFormat("<p>Xin chào,</p>");
            htmlBuilder.AppendFormat("<p>Chúng tôi xác nhận rằng đơn hàng với mã <b>{0}</b> đã được thanh toán thành công.</p>", code);
            htmlBuilder.Append("<p>Đơn hàng của bạn sẽ được chuẩn bị và giao trong thời gian sớm nhất.</p>");
            htmlBuilder.Append("<br/>");

            htmlBuilder.Append("<p>Trân trọng,</p>");
            htmlBuilder.Append("<p><b>BG Impact</b></p>");
            htmlBuilder.Append("<p>Hotline: 0123 456 789</p>");
            htmlBuilder.Append("<p>Email: support@bgimpact.com</p>");

            await SendEmailAsync(email, subject, htmlBuilder.ToString());
        }

        public async Task SendOrderDeliveredAsync(string email, string code)
        {
            string subject = "[BG Impact] Đơn hàng #" + code + " đã được giao thành công";

            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<h2>Xác nhận giao hàng thành công</h2>");
            htmlBuilder.AppendFormat("<p>Xin chào,</p>");
            htmlBuilder.AppendFormat("<p>Đơn hàng với mã <b>{0}</b> đã được giao thành công tới địa chỉ của bạn.</p>", code);
            htmlBuilder.Append("<p>Cảm ơn bạn đã tin tưởng và mua sắm tại BG Impact. Hẹn gặp lại bạn trong những lần tiếp theo!</p>");
            htmlBuilder.Append("<br/>");

            htmlBuilder.Append("<p>Trân trọng,</p>");
            htmlBuilder.Append("<p><b>BG Impact</b></p>");
            htmlBuilder.Append("<p>Hotline: 0123 456 789</p>");
            htmlBuilder.Append("<p>Email: support@bgimpact.com</p>");

            await SendEmailAsync(email, subject, htmlBuilder.ToString());
        }

        public class OrderItemModel
        {
            public string order_item_id { get; set; }
            public string template_product_name { get; set; }
            public double current_price { get; set; }
        }
    }
}