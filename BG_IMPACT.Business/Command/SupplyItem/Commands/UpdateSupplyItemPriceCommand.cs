using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.SupplyItem.Commands
{
    public class UpdateSupplyItemPriceCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid SupplyItemID { get; set; }
        [Required] 
        public float Price { get; set; }

        public class UpdateSupplyItemPriceCommandHandler : IRequestHandler<UpdateSupplyItemPriceCommand, ResponseObject>
        {
            private readonly ISupplyItemRepository _supplyItemRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public UpdateSupplyItemPriceCommandHandler(ISupplyItemRepository supplyItemRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplyItemRepository = supplyItemRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(UpdateSupplyItemPriceCommand request, CancellationToken cancellationToken)
            {
                var response = new ResponseObject();

                var context = _httpContextAccessor.HttpContext;
                string? UserId = string.Empty;

                if (context != null && (context.GetRole() == "ADMIN" || context.GetRole() == "MANAGER"))
                {
                    UserId = context.GetName();
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Bạn không có quyền thực hiện thao tác này.";
                    return response;
                }

                object param = new
                {
                    request.SupplyItemID,
                    request.Price
                };

                var result = await _supplyItemRepository.spSupplyItemUpdatePrice(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 0)
                    {
                        response.StatusCode = "200";
                        response.Message = "Thêm giá tiền sản phẩm đặt thành công.";
                        response.Data = null;
                    }
                    else if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không tìm thấy sản phẩm đặt.";
                        response.Data = null;
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không tìm thấy đơn chính của sản phẩm đặt.";
                        response.Data = null;
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Thêm giá tiền sản phẩm đặt thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }

}
