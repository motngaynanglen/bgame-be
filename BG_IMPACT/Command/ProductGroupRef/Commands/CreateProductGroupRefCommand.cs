using BG_IMPACT.Command.ProductGroup.Commands;
using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.ProductGroupRef.Commands
{
    public class CreateProductGroupRefCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid GroupId { get; set; }
        [Required]
        public string Prefix { get; set; } = string.Empty;
        [Required]
        public string GroupRefName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public class CreateProductGroupRefCommandHandler : IRequestHandler<CreateProductGroupRefCommand, ResponseObject>
        {
            public readonly IProductGroupRefRepository _productGroupRefRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateProductGroupRefCommandHandler(IProductGroupRefRepository productGroupRefRepository, IHttpContextAccessor httpContextAccessor)
            {
                _productGroupRefRepository = productGroupRefRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateProductGroupRefCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerID = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerID = context.GetName();

                    object param = new
                    {
                        request.GroupId,
                        request.Prefix,
                        request.GroupRefName,
                        request.Description,
                        ManagerID
                    };

                    var result = await _productGroupRefRepository.spProductGroupRefCreate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Tên nhóm sản phẩm đã tồn tại.";
                        }
                        else if (count == 2)
                        {
                            response.StatusCode = "404";
                            response.Message = "Prefix đã tồn tại.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm nhóm sản phẩm thành công.";
                        }

                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Thêm nhóm sản phẩm thất bại. Xin hãy thử lại sau.";
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
