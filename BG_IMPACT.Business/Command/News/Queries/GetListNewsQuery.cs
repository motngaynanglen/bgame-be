using BG_IMPACT.Business.Command.Supplier.Queries;
using BG_IMPACT.DTO.Models.PagingModels;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.News.Queries
{
    public class GetListNewsQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; } = new();

        public class GetListNewsQueryQueryHandler : IRequestHandler<GetListNewsQuery, ResponseObject>
        {
            private readonly INewsRepository _newsRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public GetListNewsQueryQueryHandler(INewsRepository newsRepository, IHttpContextAccessor contextAccessor)
            {
                _newsRepository = newsRepository;
                _contextAccessor = contextAccessor;
            }
            public async Task<ResponseObject> Handle(GetListNewsQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _contextAccessor.HttpContext;

                string? userIdString = context.GetName();
                Guid? userId = null;

                if (Guid.TryParse(userIdString, out Guid parsedId))
                {
                    userId = parsedId;
                }

                object param = new
                {
                    UserId = userId,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };


                object param2 = new
                {
                };

                var result = await _newsRepository.spNewsGetList(param);
                var list = ((IEnumerable<dynamic>)result).ToList();


                var pageData = await _newsRepository.spNewsGetListPageData(param2);
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
                    response.Message = "Không tìm thấy tin tức nào.";
                }

                return response;
            }
        }
    }
}
