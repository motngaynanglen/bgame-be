using Azure;
using BG_IMPACT.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using BG_IMPACT.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.BookList.Queries
{
    public class PerformTransactionQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid ReferenceID { get; set; }
        [Required]
        public int Type { get; set; }
        public class PerformTransactionQueryQueryHandler : IRequestHandler<PerformTransactionQuery, ResponseObject>
        {
            public readonly ITransactionRepository _transactionRepository;
            public PerformTransactionQueryQueryHandler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }

            public async Task<ResponseObject> Handle(PerformTransactionQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var clientId = "1090cdba-a0a1-4b0d-97ab-3062efa9d28e";
                var apiKey = "b4a1062e-fb09-4534-bc0f-3962810ef99a";
                var checksumKey = "4bd745d368dfdfccbb99a60a6acea20e58d4aa8fd5f83b3db173aa58a48475b8";

                var payOS = new PayOS(clientId, apiKey, checksumKey);
                var domain = "https://www.google.com";

                object param = new { 
                    request.ReferenceID,
                    request.Type
                };

                await payOS.confirmWebhook("https://bg-impact.io.vn/api/Transaction/get-online-payment-response");

                //WebhookData webhookData = payOS.verifyPaymentWebhookData(webhookBody);

                var result = await _transactionRepository.spTransactionGetItemByRefId(param);
                var list = ((IEnumerable<dynamic>)result).ToList();
                var code = list.Select(x => x.code).FirstOrDefault()?.ToString();
                var items = list.Select(x => new ItemData(x.product_name, 1, (int)x.price)).ToList();
                
                var paymentLinkRequest = new PaymentData(
                    orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                    amount: items.Sum(x => x.price),
                    description: code,
                    items: items,
                    returnUrl: domain,
                    cancelUrl: domain
                );  

                var url = await payOS.createPaymentLink(paymentLinkRequest);

                response.Data = url.checkoutUrl;

                return response;
            }
        }
    }
}
