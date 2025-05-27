using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Supplier.Commands
{
    public class UpdateSupplierCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Email { get; set; }
        public double PhoneNumber { get; set; }
        public string Status { get; set; }



        public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, ResponseObject>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateSupplierHandler(ISupplierRepository supplierRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplierRepository = supplierRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UpdatedBy = null;

                if (context != null && context.GetRole() == "MANAGER")
                {
                    UpdatedBy = context.GetName();

                    object param = new
                    {
                        request.SupplierId,
                        request.SupplierName,
                        request.Address,
                        request.Longitude,
                        request.Latitude,
                        request.Email,
                        request.PhoneNumber,
                        request.Status,
                        UpdatedBy
                    };

                    var result = await _supplierRepository.spSupplierUpdate(param);
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
