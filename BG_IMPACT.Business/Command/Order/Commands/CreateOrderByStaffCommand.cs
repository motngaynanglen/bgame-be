using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Order.Commands
{
    public class ProductItem
    {
        public Guid ProductId { get; set; }
    }
    public class CreateOrderByStaffCommand : IRequest<ResponseObject>
    {
        public Guid? CustomerId { get; set; }
        [Required]
        public List<ProductItem> Orders { get; set; } = new();
        public string PhoneNumber { get; set; } = string.Empty;

        public class CreateOrderByStaffCommandHandler : IRequestHandler<CreateOrderByStaffCommand, ResponseObject>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public CreateOrderByStaffCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _orderRepository = orderRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(CreateOrderByStaffCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;
                string? StaffId = string.Empty;
                if (context != null && context.GetRole() == "STAFF")
                {
                    _ = Guid.TryParse(context.GetName(), out Guid cusId);
                    StaffId = cusId.ToString();
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Phương thức chỉ cho Staff";
                    return response;
                }

                string ListProductIDs = string.Join(",", request.Orders
                   .SelectMany(item => Enumerable.Repeat(item.ProductId, 1)));

                object param = new
                {
                    StaffId,
                    request.CustomerId,
                    request.PhoneNumber,
                    ListProductIDs,
                };


                var result = await _orderRepository.spOrderCreateByStaff(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);
                    var message = dict["Message"].ToString() ?? "";
                    string data = dict["Data"]?.ToString() ?? "";

                    if (count == 0)
                    {
                        response.StatusCode = "200";
                        response.Message = message;
                        response.Data = dict["Data"].ToString() ?? null;
                        return response;
                    }
                    else if (count == 1)
                    {
                        response.StatusCode = "422";
                        response.Message = message;
                        if (!data.IsNullOrEmpty())
                        {
                            var invalidIds = data.Split("||", StringSplitOptions.RemoveEmptyEntries)
                                 .Select(Guid.Parse)
                                 .ToList();
                            response.Data = invalidIds;
                        }
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "403";
                        response.Message = message;
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = message;
                    }
                    else if (count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = message;
                    }
                    else if (count == 9)
                    {
                        response.StatusCode = "404";
                        response.Message = message;
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Mua hàng thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
