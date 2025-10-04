using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.News.Queries
{
    public class GetNewsByIdQuery : IRequest<ResponseObject>
    {
        public Guid NewsId { get; set; } 

        public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, ResponseObject>
        {
            private readonly INewsRepository _newsRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public GetNewsByIdQueryHandler(INewsRepository newsRepository, IHttpContextAccessor contextAccessor)
            {
                _newsRepository = newsRepository;
                _contextAccessor = contextAccessor;
            }
            public async Task<ResponseObject> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
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
                    userId,
                    request.NewsId
                };

                var result = await _newsRepository.spNewsGetById(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && dict.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy tin tức.";
                }

                return response;
            }
        }
    }
}