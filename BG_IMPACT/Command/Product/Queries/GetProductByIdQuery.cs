using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ResponseObject>
    {
        public Guid ProductID { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public GetProductByIdQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.ProductID,
                };

                var result = await _productRepository.spProductGetById(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && dict.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy sản phẩm.";
                }

                return response;
            }
        }
    }
}
