using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Product.Queries
{
    public class GetProductByTemplateAndConditionQuery : IRequest<ResponseObject>
    {
        public Guid @TemplateID { get; set; }
        public int ConditionFilter { get; set; } = 0;

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
                };

                var result = await _productRepository.spGetProductsByTemplateAndCondition(param);
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