using BG_IMPACT.Business.Command.Supplier.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.SupplyOrder.Commands
{
    public class UpdateSupplyOrderCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid SupplyOrderId { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public string Title { get; set; }


        public class UpdateSupplyOrderCommandHandler : IRequestHandler<UpdateSupplyOrderCommand, ResponseObject>
        {
            private readonly ISupplyOrderRepository _supplyOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateSupplyOrderCommandHandler(ISupplyOrderRepository supplyOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplyOrderRepository = supplyOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateSupplyOrderCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                    object param = new
                    {
                        request.SupplyOrderId,
                        request.SupplierId,
                        request.Title
                    };

                    var result = await _supplyOrderRepository.spSupplyOrderUpdate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                        if (statusCode == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Sản phẩm không tồn tại.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Cập nhật sản phẩm thành công.";
                        }
                    }
                
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Cập nhật sản phẩm thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
