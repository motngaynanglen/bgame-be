using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using Net.payOS.Types;

namespace BG_IMPACT.Command.Transaction.Commands
{
    public class GetOnlinePaymentResponseCommand : IRequest<ResponseObject>
    {
        public string Code { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public bool Success { get; set; }
        public WebhookData? Data { get; set; } 
        public string Signature { get; set; } = string.Empty;

        public class GetOnlinePaymentResponseCommandHandler : IRequestHandler<GetOnlinePaymentResponseCommand, ResponseObject>
        {
            private readonly ITransactionRepository _transactionRepository;

            public GetOnlinePaymentResponseCommandHandler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }

            public async Task<ResponseObject> Handle(GetOnlinePaymentResponseCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    Code = ((dynamic)request.Data).description
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
                    response.Message = "Thanh toán thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
