using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Product.Commands
{
    public class CreateProductUnknownCommand : IRequest<ResponseObject>
    {
        [Required]
        public string GroupName { get; set; } = string.Empty;
        [Required]
        public string Prefix { get; set; } = string.Empty;
        [Required]
        public string GroupRefName { get; set; } = string.Empty;
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public double RentPrice { get; set; }
        [Required]
        public double RentPricePerHour { get; set; }

        public class CreateProductUnknownCommandHandler : IRequestHandler<CreateProductUnknownCommand, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public CreateProductUnknownCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<ResponseObject> Handle(CreateProductUnknownCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.GroupName,
                    request.Prefix,
                    request.GroupRefName,
                    request.ProductName,
                    request.Image,
                    request.Price,
                    request.Description,
                    request.RentPrice,
                    request.RentPricePerHour,
                };

                var result = await _productRepository.spProductCreateUnknown(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Tên nhóm lớn sản phẩm đã tồn tại.";
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Tên nhóm sản phẩm đã tồn tại.";
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = "Tên sản phẩm đã tồn tại.";
                    }
                    else if (count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = "Giá thành phải lớn hơn 0.";
                    }
                    else if (count == 5)
                    {
                        response.StatusCode = "404";
                        response.Message = "Prefix này đã tồn tại";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Thêm sản phẩm thành công";
                        string productID = dict["ProductID"].ToString() ?? string.Empty;
                        response.Data = productID;
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Thêm sản phẩm thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
