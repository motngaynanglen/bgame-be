using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Product.Queries
{
    public class GetProductByCodeQuery : IRequest<ResponseObject>
    {
        public string Code { get; set; } = string.Empty;
        public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public GetProductByCodeQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Code,
                };

                var result = await _productRepository.spProductGetByCode(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                //var dict = result as IDictionary<string, object>;

                if (list != null && list.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy vật phẩm.";
                }

                return response;
            }
        }
    }
}
