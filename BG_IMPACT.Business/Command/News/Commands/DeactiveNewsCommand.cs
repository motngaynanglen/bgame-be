using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.News.Commands
{
    public class DeactiveNewsCommand : IRequest<ResponseObject>
    {
        [Required]
        public string NewsId { get; set; }


        public class DeactiveNewsCommandHandler : IRequestHandler<DeactiveNewsCommand, ResponseObject>
        {
            private readonly INewsRepository _newsRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DeactiveNewsCommandHandler(INewsRepository newsRepository, IHttpContextAccessor httpContextAccessor)
            {
                _newsRepository = newsRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(DeactiveNewsCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserId = null;

                if (context != null && context.GetRole() == "MANAGER" || context.GetRole() == "STAFF")
                {
                    UserId = context.GetName();

                    object param = new
                    {

                        request.NewsId,
                        UserId,
                    };

                    var result = await _newsRepository.spNewsDeactive(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                        if (statusCode == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Không tìm thấy bản tin.";
                        }

                        if (statusCode == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Không tìm thấy bản tin.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Deactive tin tức thành công.";
                        }
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Cập nhật tin tức thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
