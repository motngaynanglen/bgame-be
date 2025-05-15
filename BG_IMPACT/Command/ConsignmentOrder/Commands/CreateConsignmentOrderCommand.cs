using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.ConsignmentOrder.Commands
{

    public class CreateConsignmentOrderCommand : IRequest<ResponseObject>
    {
        private Guid? CustomerId { get; set; } 
        private string CustomerName { get; set; } = string.Empty;
        private string CustomerPhone{ get; set; } = string.Empty ;
        private string ProductName {  get; set; } = string.Empty ;
        private string Description { get; set; } = string.Empty;
        private string Condition { get; set; } = string.Empty;
        private string Missing { get; set; } = string.Empty;
        private float ExpectedPrice {  get; set; } = 0; 
        private float SalePrice { get; set; } = 0;
        private List<string> Images { get; set; } = [];
        public class CreateConsignmentOrderCommandHandler : IRequestHandler<CreateConsignmentOrderCommand, ResponseObject>
        {
            private readonly IConsignmentOrderRepository _consignmentOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;


            public CreateConsignmentOrderCommandHandler(IConsignmentOrderRepository consignmentOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _consignmentOrderRepository = consignmentOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateConsignmentOrderCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? StaffId = null;
                string Images = String.Join("||", request.Images);

                if (context != null && context.GetRole() == "STAFF")
                {
                    StaffId = context.GetName();
                    object param = new
                    {
                        StaffId,
                        request.CustomerId,
                        request.CustomerName,
                        request.CustomerPhone,
                        request.ProductName,
                        request.Description,  
                        request.Condition,
                        request.Missing,
                        request.ExpectedPrice,
                        request.SalePrice,
                        Images,
                    };

                    var result = await _consignmentOrderRepository.spConsignmentOrderCreateByStaff(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Không tìm thấy nhóm sản phẩm.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm sản phẩm thành công.";
                            string consignmentOrderID = dict["ConsignmentOrderID"].ToString() ?? string.Empty;
                            response.Data = consignmentOrderID;
                        }

                    }
                    else
                    {
                        response.StatusCode = "500";
                        response.Message = " Máy chủ đang bận, thêm sản phẩm thất bại.";
                    }
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Bạn không có quyền sử dụng chức năng này.";
                }


                return response;
            }
        }
    }


}
