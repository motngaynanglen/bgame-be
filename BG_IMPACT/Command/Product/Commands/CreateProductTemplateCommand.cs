using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class CreateProductTemplateCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductGroupRefId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public double RentPrice { get; set; }
        [Required]
        public double RentPricePerHour { get; set; }
        [Required]
        public int HardRank { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int NumberOfPlayerMin { get; set; }
        [Required]
        public int NumberOfPlayerMax { get; set; }
        public string Description { get; set; } = string.Empty;

        public class CreateProductTemplateCommandHandler : IRequestHandler<CreateProductTemplateCommand, ResponseObject>
        {
            private readonly IProductRepository _productRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateProductTemplateCommandHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productRepository = productRepository; 
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateProductTemplateCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerID = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerID = context.GetName();

                    object param = new
                    {
                        request.ProductGroupRefId,
                        request.ProductName,
                        request.Image,
                        request.Price,
                        request.RentPrice,
                        request.RentPricePerHour,
                        request.HardRank,
                        request.Age,
                        request.NumberOfPlayerMin,
                        request.NumberOfPlayerMax,
                        request.Description,
                        ManagerID,
                    };

                    var result = await _productRepository.spProductCreateTemplate(param);
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
