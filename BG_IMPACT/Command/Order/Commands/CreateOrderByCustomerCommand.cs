using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Order.Commands
{
    public class OrderFormItem
    {
        public Guid StoreId { get; set; }
        public Guid ProductTemplateId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderByCustomerCommand : IRequest<ResponseObject>
    {
        public Guid? CustomerID { get; set; }
        [Required]
        public List<OrderFormItem> Orders { get; set; } = new();
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public class CreateOrderByCustomerCommandHandler : IRequestHandler<CreateOrderByCustomerCommand, ResponseObject>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public CreateOrderByCustomerCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)  
            {
                _orderRepository = orderRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(CreateOrderByCustomerCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? StaffID = null;
                string Role = context?.GetRole() ?? string.Empty;

                if (context != null && context.GetRole() == "STAFF")
                {
                    StaffID = context.GetName();
                }

                if (context != null && context.GetRole() == "CUSTOMER")
                {
                    _ = Guid.TryParse(context.GetName(), out Guid cusId);
                    request.CustomerID = cusId;
                }

                string ListProductTemplateIDs = string.Join(",", request.Orders
                    .SelectMany(item => Enumerable.Repeat(item.ProductTemplateId, item.Quantity))
                );

                object param = new
                {
                    request.CustomerID,
                    StaffID,
                    ListProductTemplateIDs,
                    request.Email,
                    request.FullName,
                    request.PhoneNumber,
                    request.Address,
                    Role
                };

                var result = await _orderRepository.spOrderCreate(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Khách hàng không tồn tại.";
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Phương thức đặt không tồn tại.";
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = "Danh sách ID có dữ liệu lạ.";
                    }
                    else if (count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = "Ngày đặt và kết thúc không cùng 1 ngày.";
                    }
                    else if (count == 5)
                    {
                        response.StatusCode = "404";
                        response.Message = "Giờ đặt lớn hơn giờ kết thúc.";
                    }
                    else if (count == 6)
                    {
                        response.StatusCode = "404";
                        response.Message = "Có ID template không tồn tại.";
                    }
                    else if (count == 7)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không có đủ mặt hàng để cho thuê.";
                    }
                    else if (count == 8)
                    {
                        response.StatusCode = "404";
                        response.Message = "Dữ liệu đặt hàng nhập vào đơn không đủ.";
                    }
                    else if (count == 9)
                    {
                        response.StatusCode = "404";
                        response.Message = "Nhân viên không tồn tại.";
                    }
                    else if (count == 10)
                    {
                        response.StatusCode = "404";
                        response.Message = "Nhân viên không thuộc cửa hàng này.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Mua hàng thành công.";
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
