using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.SupplyOrder.Commands
{
    public class DeactiveSupplyOrderCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid SupplyOrderId { get; set; }


        public class DeactiveSupplyOrderCommandHandler : IRequestHandler<DeactiveSupplyOrderCommand, ResponseObject>
        {
            private readonly ISupplyOrderRepository _supplyOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DeactiveSupplyOrderCommandHandler(ISupplyOrderRepository supplyOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplyOrderRepository = supplyOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(DeactiveSupplyOrderCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.SupplyOrderId
                };

                var result = await _supplyOrderRepository.spSupplyOrderDeactive(param);
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