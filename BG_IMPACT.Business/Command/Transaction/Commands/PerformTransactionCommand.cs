using BG_IMPACT.Business.Config;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Transaction.Commands
{
    public class PerformTransactionCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ReferenceID { get; set; }
        [Required]
        public int Type { get; set; }
        public bool? IsOffline { get; set; } = false;
        public bool? IsCash { get; set; } = false;
        public class PerformTransactionCommandHandler : IRequestHandler<PerformTransactionCommand, ResponseObject>
        {
            public readonly ITransactionRepository _transactionRepository;
            public readonly PayOsCodeGenerator _codeGenerator;
            private readonly PayOsSettings _payOsSettings;
            public PerformTransactionCommandHandler(ITransactionRepository transactionRepository, IOptions<PayOsSettings> payOsSettings, PayOsCodeGenerator codeGenerator)
            {
                _transactionRepository = transactionRepository;
                _payOsSettings = payOsSettings.Value;
                _codeGenerator = codeGenerator;
            }

            public async Task<ResponseObject> Handle(PerformTransactionCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var payOS = new PayOS(_payOsSettings.ClientId, _payOsSettings.ApiKey, _payOsSettings.ChecksumKey);

                object param = new
                {
                    request.ReferenceID,
                    request.Type,
                    request.IsCash
                };

                //await payOS.confirmWebhook("https://bg-impact.io.vn/api/Transaction/get-online-payment-response");

                //WebhookData webhookData = payOS.verifyPaymentWebhookData(webhookBody);

                var result = await _transactionRepository.spTransactionGetItemByRefId(param);

                var list = ((IEnumerable<dynamic>)result).ToList();
                if (!list.Any())
                {
                    response.StatusCode = "404";
                    response.Data = "Không tìm thấy đơn hàng";
                    return response;
                }
                if (((IDictionary<string, object>)list[0]).ContainsKey("Status"))
                {
                    string? status = ((IDictionary<string, object>)list[0])["Status"]?.ToString();

                    if (status == "409") // Đã tồn tại link thanh toán
                    {
                        var existUrl = new
                        {
                            Code = ((IDictionary<string, object>)list[0])["PaymentId"]?.ToString(),
                            checkoutUrl = ((IDictionary<string, object>)list[0])["PaymentLink"]?.ToString(),
                        };

                        response.StatusCode = "200";
                        response.Message = "";
                        response.Data = existUrl;
                        return response;
                    }
                }
                var code = list.Select(x => x.code).FirstOrDefault()?.ToString();
                var items = list.Select(x => new ItemData(x.product_name, 1, (int)x.price)).ToList();

                if (request.IsCash == true)
                {
                    object paramCode = new
                    {
                        Code = code
                    };

                    await _transactionRepository.MarkOrderAsPaid(paramCode);
                    response.StatusCode = "200";
                    response.Data = "Đã thanh toán thành công.";
                    return response;
                }

                long orderCode = _codeGenerator.GenerateCode();

                var paymentRequest = new PaymentData(
                    orderCode: orderCode,
                    amount: items.Sum(x => x.price),
                    description: code,
                    items: items,
                    returnUrl: _payOsSettings.ReturnUrl,
                    cancelUrl: _payOsSettings.CancelUrl
                );

                var url = await payOS.createPaymentLink(paymentRequest);

                object updateParam = new
                {
                    ReferenceID = request.ReferenceID,
                    PaymentId = orderCode,
                    PaymentLink = url.checkoutUrl,
                    ExpiredAt = DateTime.UtcNow.AddMinutes(10)
                };

                await _transactionRepository.spTransactionUpdatePaymentRef(updateParam);

                object resParam = new
                {
                    code = url.orderCode,
                    url.checkoutUrl,
                    url.qrCode,
                };

                response.StatusCode = "200";
                response.Data = resParam;

                return response;
            }
        }
    }
}
