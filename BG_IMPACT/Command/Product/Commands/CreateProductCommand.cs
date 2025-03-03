using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class CreateProductCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductGroupRefId { get; set; }
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public double Number { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public CreateProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<ResponseObject> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.ProductGroupRefId,
                    request.StoreId,
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

                return response;
            }
        }
    }
}
