using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.News.Commands
{
    public class UpdateNewsCommand : IRequest<ResponseObject>
    {
        [Required]
        public string NewsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public double Content { get; set; }
        [Required]
        public string Status { get; set; }
        

        public class CreateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, ResponseObject>
        {
            private readonly INewsRepository _newsRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateNewsCommandHandler(INewsRepository newsRepository, IHttpContextAccessor httpContextAccessor)
            {
                _newsRepository = newsRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
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
                        request.Title,
                        request.Content,
                        request.Status,
                        UserId,
                    };

                    var result = await _newsRepository.spNewsUpdate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                        if (statusCode == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Không tìm thấy bản tin.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Cập nhật tin tức thành công.";
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
