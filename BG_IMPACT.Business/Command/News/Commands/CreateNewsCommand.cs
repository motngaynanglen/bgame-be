using BG_IMPACT.Business.Command.Supplier.Commands;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.News.Commands
{
    public class CreateNewsCommand : IRequest<ResponseObject>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        
        public string Image { get; set; }

        public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, ResponseObject>
        {
            private readonly INewsRepository _newsRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateNewsCommandHandler(INewsRepository newsRepository, IHttpContextAccessor httpContextAccessor)
            {
                _newsRepository = newsRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserId = null;

                if (context != null && context.GetRole() == "MANAGER" || context.GetRole() == "STAFF")
                {
                    UserId = context.GetName();

                    object param = new
                    {
                        request.Title,
                        request.Content,
                        request.Image,
                        UserId,
                    };

                    var result = await _newsRepository.spNewsCreate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 1)
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm tin tức thành công .";
                        }

                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Thêm tin tức bại. Xin hãy thử lại sau.";
                    }
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Bạn không có quyền sử dụng chức năng này.";
                }

                return response;
            }
        }
    }
}
