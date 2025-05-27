using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Product.Commands
{
    public class CreateProductCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductTemplateId { get; set; }
        [Required]
        public double Number { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseObject>
        {
            private readonly IProductRepository _productRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateProductCommandHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productRepository = productRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerID = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerID = context.GetName();

                    object param = new
                    {
                        request.ProductTemplateId,
                        ManagerID,
                        request.Number
                    };

                    var result = await _productRepository.spProductCreate(param);
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
                            response.Message = "Số lượng mặt hàng phải lớn hơn 0";
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
