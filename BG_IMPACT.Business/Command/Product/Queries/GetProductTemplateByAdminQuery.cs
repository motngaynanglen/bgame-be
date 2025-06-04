using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Product.Queries
{
    public class GetProductTemplateByAdminQuery : IRequest<ResponseObject>
    {
        

        public class GetProductTemplateByAdminQueryHandler : IRequestHandler<GetProductTemplateByAdminQuery, ResponseObject>
        {
            private readonly IProductRepository _productRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetProductTemplateByAdminQueryHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productRepository = productRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetProductTemplateByAdminQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? string.Empty;
                object param = new
                {
                    UserID
                };

                var result = await _productRepository.spProductGetTemplateByAdmin(param);
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
