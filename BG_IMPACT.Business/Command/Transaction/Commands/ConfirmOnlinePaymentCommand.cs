using System.Security.Cryptography;
using System.Text;

namespace BG_IMPACT.Business.Command.Transaction.Commands
{
    public class ConfirmOnlinePaymentCommand : IRequest<ResponseObject>
    {
        public string Code { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public bool Success { get; set; }
        public WebhookData? Data { get; set; }
        public string Signature { get; set; } = string.Empty;

        public class ConfirmOnlinePaymentCommandHandler : IRequestHandler<ConfirmOnlinePaymentCommand, ResponseObject>
        {
            private readonly ITransactionRepository _transactionRepository;

            public ConfirmOnlinePaymentCommandHandler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }

            public async Task<ResponseObject> Handle(ConfirmOnlinePaymentCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

               
                if (request.Data == null)
                {
                    response.StatusCode = "400";
                    response.Message = "Thiếu dữ liệu thanh toán.";
                    return response;
                }
                var checksumKey = "4bd745d368dfdfccbb99a60a6acea20e58d4aa8fd5f83b3db173aa58a48475b8";
                string rawData = $"amount={request.Data.amount}&description={request.Data.description}&orderCode={request.Data.orderCode}";
                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey));
                var computedHash = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData)))
                                            .Replace("-", "").ToLower();

                //if (computedHash != request.Signature)
                //{
                //    response.StatusCode = "403";
                //    response.Message = "Dữ liệu không hợp lệ (sai chữ ký).";
                //    return response;
                //}
                //response.Data = request.Data.ToString();
                object param = new
                {
                    Code = request.Data.description  // description chính là mã đơn hàng
                };

                var result = await _transactionRepository.spCheckOnlinePayment(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Đơn thuê không tồn tại.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Thanh toán thành công.";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Thanh toán thất bại. Xin hãy liên hệ admin.";
                }

                return response;
            }
        }
    }
}
