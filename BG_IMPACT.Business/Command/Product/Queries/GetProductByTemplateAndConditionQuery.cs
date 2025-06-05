namespace BG_IMPACT.Business.Command.Product.Queries
{
    public class GetProductByTemplateAndConditionQuery : IRequest<ResponseObject>
    {
        public Guid TemplateID { get; set; }
        public int ConditionFilter { get; set; } = 0;
        public Paging Paging { get; set; } = new();

        public class GetProductByTemplateAndConditionQueryHandler : IRequestHandler<GetProductByTemplateAndConditionQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetProductByTemplateAndConditionQueryHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productRepository = productRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetProductByTemplateAndConditionQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? string.Empty;
                object param = new
                {
                    request.TemplateID,
                    UserID,
                    request.ConditionFilter,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                object param2 = new
                {
                    request.TemplateID,
                    UserID,
                    request.ConditionFilter
                };

                var result = await _productRepository.spGetProductsByTemplateAndCondition(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _productRepository.spGetProductsByTemplateAndConditionPageData(param2);
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