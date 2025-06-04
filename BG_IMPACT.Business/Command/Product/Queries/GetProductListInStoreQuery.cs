using BG_IMPACT.DTO.Models;

namespace BG_IMPACT.Business.Command.Product.Queries
{
    public class GetProductListInStoreQuery : IRequest<ResponseObject>
    {
        public string Search { get; set; } = string.Empty;
        public Filter? Filter { get; set; }
        public Paging Paging { get; set; } = new Paging();

        public class GetProductListInStoreQueryHandler : IRequestHandler<GetProductListInStoreQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetProductListInStoreQueryHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productRepository = productRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetProductListInStoreQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;
                string? Role = context?.GetRole() ?? null;

                object param = new
                {
                    request.Search,
                    UserID,
                    Role,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                object param2 = new
                {
                    request.Search,
                    UserID,
                    Role
                };

                var result = await _productRepository.spProductGetListInStore(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _productRepository.spProductGetListInStorePageData(param2);
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
