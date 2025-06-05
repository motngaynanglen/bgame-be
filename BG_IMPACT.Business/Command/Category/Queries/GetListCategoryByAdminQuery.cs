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
        public Paging Paging { get; set; } = new();
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

                object param = new
                {
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                object param2 = new
                {
                };

                var result = await _categoryRepository.spCategoryGetListByAdmin(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _categoryRepository.spCategoryGetListByAdminPageData(param2);
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
                    response.Message = "Không tìm thấy danh mục nào.";
                }

                return response;
            }
        }
    }
}
