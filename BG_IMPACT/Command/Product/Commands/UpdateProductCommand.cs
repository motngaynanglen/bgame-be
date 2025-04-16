using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class UpdateProductCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductID { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
        public float Price { get; set; }
        public float RentPrice { get; set; }
        public float RentPricePerHour { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public int Age { get; set; }
        public int NumberOfPlayerMin { get; set; }
        public int NumberOfPlayerMax { get; set; }
        public int HardRank { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseObject>
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateProductHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            ResponseObject response = new();

            var context = _httpContextAccessor.HttpContext;

            string? ManagerID = null;

            if (context != null && context.GetRole() == "MANAGER")
            {
                ManagerID = context.GetName();

                object param = new
                {
                    request.ProductID,
                    request.ProductName,
                    request.Image,
                    request.Price,
                    request.RentPrice,
                    request.RentPricePerHour,
                    request.Publisher,
                    request.Age,
                    request.NumberOfPlayerMin,
                    request.NumberOfPlayerMax,
                    request.HardRank,
                    request.Description,
                    ManagerID
                };

            var result = await _productRepository.spProductUpdate(param);
            var dict = result as IDictionary<string, object>;

            if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
            {
                _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                    if (statusCode == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Sản phẩm không tồn tại.";
                    }
                    else if (statusCode == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không tìm thấy Manager hoặc cửa hàng của sản phẩm không tồn tại.";
                    }
                    else if (statusCode == 4)
                    {
                        response.StatusCode = "400";
                        response.Message = "Bạn không có quyền cập nhật sản phẩm của cửa hàng khác.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Cập nhật sản phẩm thành công.";
                    }
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
