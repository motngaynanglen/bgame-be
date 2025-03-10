using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class ChangeProductToRent : IRequest<ResponseObject>
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        public class ChangeProductToRentHandler : IRequestHandler<ChangeProductToRent, ResponseObject>
        {
            public readonly IProductRepository _productRepository;

            public ChangeProductToRentHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(ChangeProductToRent request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Code
                };

                var result = await _productRepository.spProductChangeToRent(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Sản phẩm không tồn tại.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Chuyển sản phẩm cho thuê thành công";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Chuyển sản phẩm cho thuê thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
