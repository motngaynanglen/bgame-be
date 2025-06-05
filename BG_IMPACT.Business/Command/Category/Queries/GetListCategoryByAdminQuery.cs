using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Category.Queries
{
    public class GetListCategoryByAdminQuery : IRequest<ResponseObject>
    {
        
        public class GetListCategoryByAdminQueryHandler : IRequestHandler<GetListCategoryByAdminQuery, ResponseObject>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetListCategoryByAdminQueryHandler(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
            {
                _categoryRepository = categoryRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetListCategoryByAdminQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;
                string? UserId = null;

                if (context != null && context.GetRole() == "ADMIN")
                {
                    UserId = context.GetName();
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Bạn không có quyền thực hiện thao tác này.";
                    return response;
                }

                

                var result = await _categoryRepository.spCategoryGetListByAdmin();
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
                    response.Message = "Không tìm thấy supplier nào.";
                }
                return response;
            }
        }
    }
}
