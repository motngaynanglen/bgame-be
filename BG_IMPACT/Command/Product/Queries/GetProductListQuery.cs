using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Product.Queries
{
    public class GetProductListQuery : IRequest<object>
    {
        public string Search {  get; set; } = string.Empty;
        public List<string> Filter { get; set; } = [];

        public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, object>
        {
            private readonly IProductRepository _productRepository;

            public GetProductListQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<object> Handle(GetProductListQuery request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    request.Search,
                    request.Filter
                };

                var result = await _productRepository.spProductGetList(param);

                return result;
            }
        }
    }
}
