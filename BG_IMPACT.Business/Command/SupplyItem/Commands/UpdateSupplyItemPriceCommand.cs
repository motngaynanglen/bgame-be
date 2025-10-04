using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BG_IMPACT.Business.Command.SupplyItem.Commands
{
    public class UpdatePriceModel
    {
        [Required(ErrorMessage = "SupplyItemID là bắt buộc.")]
        public Guid SupplyItemID { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(1000, float.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 1000.")]
        public float Price { get; set; }
    }

    public class UpdateSupplyItemPriceCommand : IRequest<ResponseObject>
    {
        [Required(ErrorMessage = "SupplyOrderId là bắt buộc.")]
        public Guid SupplyOrderID { get; set; }

        [Required(ErrorMessage = "Danh sách sản phẩm là bắt buộc.")]
        [MinLength(1, ErrorMessage = "Danh sách sản phẩm phải có ít nhất 1 sản phẩm.")]
        public List<UpdatePriceModel> Items { get; set; } = new List<UpdatePriceModel>();

        public class UpdateSupplyItemPriceCommandHandler : IRequestHandler<UpdateSupplyItemPriceCommand, ResponseObject>
        {
            private readonly ISupplyItemRepository _supplyItemRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateSupplyItemPriceCommandHandler(
                ISupplyItemRepository supplyItemRepository,
                IHttpContextAccessor httpContextAccessor)
            {
                _supplyItemRepository = supplyItemRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateSupplyItemPriceCommand request, CancellationToken cancellationToken)
            {
                var response = new ResponseObject();

                var context = _httpContextAccessor.HttpContext;
                string? ManagerID = string.Empty;

                if (context != null && (context.GetRole() == "ADMIN" || context.GetRole() == "MANAGER"))
                {
                    ManagerID = context.GetName();
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Bạn không có quyền thực hiện thao tác này.";
                    return response;
                }

                var duplicateIds = request.Items
                    .GroupBy(i => i.SupplyItemID)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateIds.Any())
                {
                    response.StatusCode = "404";
                    response.Message = $"Có SupplyItemID bị trùng: {string.Join(", ", duplicateIds)}";
                    return response;
                }

                int successCount = 0;
                int total = request.Items.Count;
                var messages = new List<string>();

                foreach (var item in request.Items)
                {
                    object param = new
                    {
                        ManagerID,
                        request.SupplyOrderID,
                        item.SupplyItemID,
                        item.Price
                    };

                    var result = await _supplyItemRepository.spSupplyItemUpdatePrice(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out long status))
                    {
                        if (status == 0)
                        {
                            successCount++;
                            messages.Add($"SupplyItemID {item.SupplyItemID}: cập nhật thành công.");
                        }
                        else if (status == 1)
                        {
                            messages.Add($"SupplyItemID {item.SupplyItemID}: không tìm thấy sản phẩm đặt.");
                        }
                        else if (status == 2)
                        {
                            messages.Add($"SupplyItemID {item.SupplyItemID}: ID sản phẩm không thuộc đơn này.");
                        }
                        else if (status == 3)
                        {
                            messages.Add($"SupplyItemID {item.SupplyItemID}: Không có quyền xử lý thông tin đơn này.");
                        }
                    }
                    else
                    {
                        messages.Add($"SupplyItemID {item.SupplyItemID} cập nhật thất bại, vui lòng thử lại sau.");
                    }
                }

                response.StatusCode = "200";
                response.Message = $"Kết quả: {successCount}/{total} thành công.\n" + string.Join("\n", messages);

                return response;
            }
        }

    }

}
