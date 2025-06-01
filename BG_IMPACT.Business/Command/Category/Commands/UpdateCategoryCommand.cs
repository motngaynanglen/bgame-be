using BG_IMPACT.Business.Command.News.Commands;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Category.Commands
{
    public class UpdateCategoryCommand : IRequest<ResponseObject>
    {
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Status { get; set; }

        public class UpdateCategoryCommandCommandHandler : IRequestHandler<UpdateCategoryCommand, ResponseObject>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateCategoryCommandCommandHandler(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
            {
                _categoryRepository = categoryRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserId = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    UserId = context.GetName();

                    object param = new
                    {

                        request.CategoryId,
                        request.Name,
                        request.Status,
                        UserId,
                    };

                    var result = await _categoryRepository.spCategoryUpdate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                        if (statusCode == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Không tìm thấy Category.";
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
                    response.Message = "Cập nhật Category thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
