using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Order.Commands
{
    public class UpdateOrderIsHubCommand : IRequest<ResponseObject>
    {
        public Guid Id { get; set; }

        public class UpdateOrderIsHubCommandHandler : IRequestHandler<UpdateOrderIsHubCommand, ResponseObject>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateOrderIsHubCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _orderRepository = orderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateOrderIsHubCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? AccountID = context?.GetName() ?? string.Empty;
                object parameters = new
                {
                    AccountID,
                    OrderID = request.Id
                };
                var result = await _orderRepository.spOrderUpdateIsHub(parameters);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    string? Message = dict["Message"].ToString() ?? string.Empty;

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }
                    else if (count == 2)
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
