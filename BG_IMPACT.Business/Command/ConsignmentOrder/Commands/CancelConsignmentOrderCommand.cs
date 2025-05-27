using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.ConsignmentOrder.Commands
{

    public class CancelConsignmentOrderCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ConsignmentOrderId { get; set; }
        public class CancelConsignmentOrderCommandHandler : IRequestHandler<CancelConsignmentOrderCommand, ResponseObject>
        {
            private readonly IConsignmentOrderRepository _consignmentOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;


            public CancelConsignmentOrderCommandHandler(IConsignmentOrderRepository consignmentOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _consignmentOrderRepository = consignmentOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CancelConsignmentOrderCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? StaffId = null;

                if (context != null && context.GetRole() == "STAFF")
                {
                    StaffId = context.GetName();
                    object param = new
                    {
                        StaffId,
                        request.ConsignmentOrderId,
                    };

                    var result = await _consignmentOrderRepository.spConsignmentOrderCancelByStaff(param);
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
                            response.Message = "Hủy sản phẩm thành công.";
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
