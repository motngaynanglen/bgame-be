using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.ConsignmentOrder.Commands
{

    public class UpdateConsignmentOrderCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ConsignmentOrderId { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Condition { get; set; } = 1;
        public string Missing { get; set; } = string.Empty;
        public float ExpectedPrice { get; set; } = 0;
        public float SalePrice { get; set; } = 0;
        public List<string> Images { get; set; } = [];
        public class UpdateConsignmentOrderCommandHandler : IRequestHandler<UpdateConsignmentOrderCommand, ResponseObject>
        {
            private readonly IConsignmentOrderRepository _consignmentOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;


            public UpdateConsignmentOrderCommandHandler(IConsignmentOrderRepository consignmentOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _consignmentOrderRepository = consignmentOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateConsignmentOrderCommand request, CancellationToken cancellationToken)
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
                        request.ConsignmentOrderId,
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

                    var result = await _consignmentOrderRepository.spConsignmentOrderUpdateByStaff(param);
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
