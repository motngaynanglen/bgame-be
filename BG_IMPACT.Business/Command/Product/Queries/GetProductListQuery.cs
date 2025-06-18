using BG_IMPACT.DTO.Models.PagingModels;

namespace BG_IMPACT.Business.Command.Product.Queries
{
    public class GetProductListQuery : IRequest<ResponseObject>
    {
        public string Search { get; set; } = string.Empty;
        public Filter? Filter { get; set; } 
        public Paging Paging { get; set; } = new();

        public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;

            public GetProductListQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseObject> Handle(GetProductListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Search,
                    request.Paging.PageNum,
                    request.Paging.PageSize,
                    request.Filter?.MinPrice,
                    request.Filter?.MaxPrice,
                    request.Filter?.MinNumberOfPlayers,
                    request.Filter?.MaxNumberOfPlayer,
                    request.Filter?.Age,
                    request.Filter?.Duration,
                    request.Filter?.CategoryList,
                    request.Filter?.InStock
                };

                object param2 = new
                {
                    request.Search
                };

                var result = await _productRepository.spProductGetList(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _productRepository.spProductGetListPageData(param2);
                var dict = pageData as IDictionary<string, object>;
                long count = 0;

                if (dict != null && Int64.TryParse(dict["TotalRows"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["TotalRows"].ToString(), out count);
                }

                if (list.Count > 0)
                {
                    long pageCount = count / request.Paging.PageSize;

                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                    response.Paging = new PagingModel
                    {
                        PageNum = request.Paging.PageNum,
                        PageSize = request.Paging.PageSize,
                        PageCount = count % request.Paging.PageSize == 0 ? pageCount : pageCount + 1
                    };
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
