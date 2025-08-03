using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Product.Commands
{
    public class AddProductFromSupplyItemCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid SupplyItemID { get; set; }
        [Required]
        public Guid ProductTemplateID { get; set; }

        public class AddProductFromSupplyItemCommandHandler : IRequestHandler<AddProductFromSupplyItemCommand, ResponseObject>
        {
            private readonly IProductRepository _productRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public AddProductFromSupplyItemCommandHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productRepository = productRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(AddProductFromSupplyItemCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerID = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerID = context.GetName();

                    object param = new
                    {
                        request.SupplyItemID,
                        request.ProductTemplateID,
                        ManagerID
                    };

                    var result = await _productRepository.spProductAddFromSupplyItem(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Sản phẩm không tồn tại.";
                        }
                        else if (count == 2)
                        {
                            response.StatusCode = "404";
                            response.Message = "Cửa hàng không tồn tại.";
                        }
                        else if (count == 3)
                        {
                            response.StatusCode = "404";
                            response.Message = "Sản phẩm đã được thêm trước đó hoặc đã bị hủy bỏ.";
                        }
                        else if (count == 4)
                        {
                            response.StatusCode = "404";
                            response.Message = "Quản lý không tồn tại.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm sản phẩm thành công";
                        }
                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Thêm sản phẩm thất bại. Xin hãy thử lại sau.";
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
