using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Order.Commands
{
    public class UpdateStatusOrderToReceivedCommand : IRequest<ResponseObject>
    {
        public Guid? OrderID { get; set; }

        public class UpdateStatusOrderToReceivedCommandHandler : IRequestHandler<UpdateStatusOrderToReceivedCommand, ResponseObject>
        {
            private readonly IOrderRepository _orderRepository;

            private readonly IHttpContextAccessor _httpContextAccessor;


            public UpdateStatusOrderToReceivedCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _orderRepository = orderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateStatusOrderToReceivedCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? AccountID = context?.GetName() ?? string.Empty;
                object parameters = new
                {
                    AccountID,
                    request.OrderID

                };
                var result = await _orderRepository.spOrderUpdateStatusToReceived(parameters);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    string? Message = dict["Message"].ToString() ?? string.Empty;

                    if (count == 1 || count == 2 || count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = Message;
                    }

                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Cập nhật thông tin thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
