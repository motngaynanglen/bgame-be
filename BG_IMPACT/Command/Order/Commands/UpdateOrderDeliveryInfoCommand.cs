using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Order.Commands
{
    public class UpdaterOrderDeliveryInfoCommand : IRequest<ResponseObject>
    {
        public Guid? OrderID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsDelivery { get; set; } = true; //true: shipping (default) ; false: get at store
        public string DeliveryCode { get; set; } = string.Empty;
        public string DeliveryBrand { get; set; } = string.Empty;


        public class UpdaterOrderDeliveryInfoCommandHandler : IRequestHandler<UpdaterOrderDeliveryInfoCommand, ResponseObject>
        {
            private readonly IOrderRepository _orderRepository;

            private readonly IHttpContextAccessor _httpContextAccessor;


            public UpdaterOrderDeliveryInfoCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _orderRepository = orderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdaterOrderDeliveryInfoCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;
                if (!request.IsDelivery)
                {
                    request.DeliveryCode = "";
                    request.DeliveryBrand = "";
                }
                string? AccountID = context?.GetName() ?? string.Empty;
                object parameters = new
                {
                    AccountID,
                    request.OrderID,
                    request.Email,
                    request.FullName,
                    request.PhoneNumber,
                    request.Address,
                    request.IsDelivery,
                    request.DeliveryCode,
                    request.DeliveryBrand,

                };
                var result = await _orderRepository.spOrderUpdateDeliveryInfo(parameters);
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
                        response.StatusCode = "403";
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