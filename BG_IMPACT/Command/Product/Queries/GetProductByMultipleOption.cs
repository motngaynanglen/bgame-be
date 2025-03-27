using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Product.Queries
{
    public class GetProductByMultipleOption : IRequest<ResponseObject>
    {
        public Guid ProductID { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool IsProductID { get; set; }

        public class GetProductByMultipleOptionHandler : IRequestHandler<GetProductByMultipleOption, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public GetProductByMultipleOptionHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(GetProductByMultipleOption request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.ProductID,
                    request.Code,
                    request.IsProductID,
                };

                var result = await _productRepository.spProductGetByMultipleOption(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                if (list.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy sản phẩm nào.";
                }

                return response;
            }
        }
    }
}
