using BG_IMPACT.Command.Product.Commands;
using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Supplier.Commands
{
    public class CreateSupplierCommand : IRequest<ResponseObject>
    {
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; } 
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, ResponseObject>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplierRepository = supplierRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? ManagerID = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    ManagerID = context.GetName();

                    object param = new
                    {
                        request.SupplierName,
                        request.Address,
                        request.Longitude,
                        request.Latitude,
                        request.Email,
                        request.PhoneNumber,
                        ManagerID,
                    };

                    var result = await _supplierRepository.spSupplierCreate(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                    {
                        _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                        if (count == 0)
                        {
                            response.StatusCode = "200";
                            response.Message = "Thêm sản phẩm thành công.";
                            string supplierID = dict["SupplierId"].ToString() ?? string.Empty;
                            response.Data = supplierID;
                        }

                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Thêm sản phẩm thất bại. Xin hãy thử lại sau.";
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
