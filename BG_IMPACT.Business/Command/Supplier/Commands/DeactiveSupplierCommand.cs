using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Supplier.Commands
{
    public class DeactiveSupplierCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid SupplierId { get; set; }
        public class DeactiveSupplierCommandHandler : IRequestHandler<DeactiveSupplierCommand, ResponseObject>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DeactiveSupplierCommandHandler(ISupplierRepository supplierRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplierRepository = supplierRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(DeactiveSupplierCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerId = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerId = context.GetName();

                    object param = new
                    {
                        request.SupplierId,
                        ManagerId
                    };

                    var result = await _supplierRepository.spSupplierDeactive(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long statusCode);

                        if (statusCode == 1)
                        {
                            response.StatusCode = "404";
                            response.Message = "Sản phẩm không tồn tại.";
                        }
                        else
                        {
                            response.StatusCode = "200";
                            response.Message = "Cập nhật sản phẩm thành công.";
                        }
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Cập nhật sản phẩm thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
