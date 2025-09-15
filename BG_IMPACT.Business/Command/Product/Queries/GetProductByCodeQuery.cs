namespace BG_IMPACT.Business.Command.Product.Queries
{
    public class GetProductByCodeQuery : IRequest<ResponseObject>
    {
        public string Code { get; set; } = string.Empty;

        public ProductType ProductType { get; set; }
        public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public GetProductByCodeQueryHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productRepository = productRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;
                string AccountId = string.Empty;
                string Role = string.Empty;
                if (context != null)
                {
                    _ = Guid.TryParse(context.GetName(), out Guid cusId);
                    AccountId = cusId.ToString();
                    Role = context.GetRole();
                }
                object param = new
                {
                    AccountId = new Guid(AccountId), 
                    Role,
                    request.Code,
                    ProductType = request.ProductType.ToString() 
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
