using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.ProductGroup.Commands
{
    public class CreateProductGroupCommand : IRequest<ResponseObject>
    {
        [Required]
        public string GroupName { get; set; } = string.Empty;


        public class CreateProductGroupCommandHandler : IRequestHandler<CreateProductGroupCommand, ResponseObject>
        {
            public readonly IProductGroupRepository _productGroupRepository;
            public readonly IHttpContextAccessor _httpContextAccessor;

            public CreateProductGroupCommandHandler(IProductGroupRepository productGroupRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productGroupRepository = productGroupRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerID = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerID = context.GetName();

                    object param = new
                    {
                        request.GroupName,
                        ManagerID
                    };

                    var result = await _productGroupRepository.spProductGroupCreate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Tên nhóm lớn sản phẩm đã tồn tại.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm nhóm lớn sản phẩm thành công.";
                        }

                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Thêm nhóm lớn sản phẩm thất bại. Xin hãy thử lại sau.";
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
