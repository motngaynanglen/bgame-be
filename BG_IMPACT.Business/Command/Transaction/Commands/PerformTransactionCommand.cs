using CloudinaryDotNet;
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
        public bool? IsCash { get; set; } =false;
        public class PerformTransactionCommandHandler : IRequestHandler<PerformTransactionCommand, ResponseObject>
        {
            public readonly ITransactionRepository _transactionRepository;
            public PerformTransactionCommandHandler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }

            public async Task<ResponseObject> Handle(PerformTransactionCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var clientId = "1090cdba-a0a1-4b0d-97ab-3062efa9d28e";
                var apiKey = "b4a1062e-fb09-4534-bc0f-3962810ef99a";
                var checksumKey = "4bd745d368dfdfccbb99a60a6acea20e58d4aa8fd5f83b3db173aa58a48475b8";

                var payOS = new PayOS(clientId, apiKey, checksumKey);
                var domain = "https://www.google.com";

                object param = new
                {
                    request.ReferenceID,
                    request.Type,
                    request.IsOffline
                };

                //await payOS.confirmWebhook("https://bg-impact.io.vn/api/Transaction/get-online-payment-response");

                //WebhookData webhookData = payOS.verifyPaymentWebhookData(webhookBody);

                var result = await _transactionRepository.spTransactionGetItemByRefId(param);
                var list = ((IEnumerable<dynamic>)result).ToList();
                var code = list.Select(x => x.code).FirstOrDefault()?.ToString();
                var items = list.Select(x => new ItemData(x.product_name, 1, (int)x.price)).ToList();

                if (request.IsCash == null || request.IsCash == false)
                {
                    var paymentLinkRequest = new PaymentData(
                    orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                    amount: items.Sum(x => x.price),
                    description: code,
                    items: items,
                    returnUrl: domain,
                    cancelUrl: domain
                );

                    var url = await payOS.createPaymentLink(paymentLinkRequest);
                    object resParam = new
                    {
                        url.checkoutUrl,
                        url.qrCode,
                    };

                    response.StatusCode = "200";
                    response.Data = resParam;
                }
                else
                { 
                    object param2 = new
                    {
                        Code = code
                    };

                    var result2 = await _transactionRepository.spCheckOnlinePayment(param2);

                    response.StatusCode = "200";
                    response.Data = "Đã thanh toán thành công.";
                }

                return response;
            }
        }
    }
}
