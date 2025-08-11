using BG_IMPACT.Business.Command.Order.Commands;
using BG_IMPACT.Business.Config;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Transaction.Commands
{
    public class CancelTransactionCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ReferenceID { get; set; }
        public class CancelOrderCommandHandler : IRequestHandler<CancelTransactionCommand, ResponseObject>
        {
            private readonly ITransactionRepository _transactionRepository;
            private readonly PayOsSettings _payOsSettings;

            public CancelOrderCommandHandler(ITransactionRepository transactionRepository, IOptions<PayOsSettings> payOsSettings)
            {
                _transactionRepository = transactionRepository;
                _payOsSettings = payOsSettings.Value;
            }

            public async Task<ResponseObject> Handle(CancelTransactionCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                // 1. Lấy thông tin đơn hàng
                object queryParam = new
                {
                    request.ReferenceID
                };
                var result = await _transactionRepository.spTransactionGetByRefId(queryParam);

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
                    string? paymentId = ((IDictionary<string, object>)list[0])["PaymentId"]?.ToString();
                    if (status == "SUSSESS") // Giao dịch đã hoàn thành
                    {
                        // 2. Nếu có PaymentId, gọi API hủy PayOS
                        if (!string.IsNullOrEmpty(paymentId))
                        {
                            var payOS = new PayOS(
                                _payOsSettings.ClientId,
                                _payOsSettings.ApiKey,
                                _payOsSettings.ChecksumKey
                            );
                            
                            try
                            {
                                _ = long.TryParse(paymentId, out long id);
                                await payOS.cancelPaymentLink(id);
                            }
                            catch (Exception ex)
                            {
                                // Nếu PayOS hủy thất bại, log lại nhưng vẫn tiếp tục hủy trong DB
                                Console.WriteLine($"Hủy PayOS thất bại: {ex.Message}");
                            }
                        }
                    }
                }

                
                

                // 3. Cập nhật trạng thái trong DB
                //await _transactionRepository.UpdateOrderStatus(request.ReferenceID, "Cancelled");

                response.StatusCode = "200";
                response.Data = "Đã hủy đơn hàng thành công";
                return response;
            }
        }
    }
}
