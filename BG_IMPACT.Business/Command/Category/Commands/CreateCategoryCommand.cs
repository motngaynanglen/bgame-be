using BG_IMPACT.Business.Command.News.Commands;
using BG_IMPACT.Repository.Repositories.Implementations;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Category.Commands
{
    public class CreateCategoryCommand : IRequest<ResponseObject>
    {
        [Required]
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseObject>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
            {
                _categoryRepository = categoryRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserId = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    UserId = context.GetName();

                    object param = new
                    {
                        request.Name,
                        UserId,
                    };

                    var result = await _categoryRepository.spCategoryCreate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 0)
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm Category thành công .";
                        }

                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Thêm Category bại. Xin hãy thử lại sau.";
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
