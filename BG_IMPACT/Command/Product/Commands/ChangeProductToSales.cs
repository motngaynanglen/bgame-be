using Azure;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Product.Commands
{
    public class ChangeProductToSales : IRequest<ResponseObject>
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        public class ChangeProductToSalesHandler : IRequestHandler<ChangeProductToSales, ResponseObject>
        {
            public readonly IProductRepository _productRepository;

            public ChangeProductToSalesHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(ChangeProductToSales request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Code
                };

                var result = await _productRepository.spProductChangeToSales(param);
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
                        response.Message = "Đã tồn tại đơn thuê, không thể chuyển lại thành sản phẩm mua bán.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Chuyển sản phẩm mua bán thành công";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Chuyển sản phẩm mua bán thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
