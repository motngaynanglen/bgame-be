using BG_IMPACT.Business.Command.News.Queries;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Category.Queries
{
    public class GetListCategoryQuery : IRequest<ResponseObject>
    {

        public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, ResponseObject>
        {
            private readonly ICategoryRepository _categoryRepository;

            public GetListCategoryQueryHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }
            public async Task<ResponseObject> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();


                var result = await _categoryRepository.spCategoryGetList();
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
                    response.Message = "Không tìm thấy bản tin nào.";
                }
                return response;
            }
        }
    }
}
