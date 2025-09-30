using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BG_IMPACT.Business.Command.Account.Commands
{
    public class AddressModel
    {

        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên phải có độ dài từ 1 đến 50 ký tự.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Địa chỉ phải có độ dài từ 1 đến 200 ký tự.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại Việt Nam phải bắt đầu bằng 0 và có 10 chữ số.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
    public class UpdateCustomerAddressCommand : IRequest<ResponseObject>
    {
        [Required]
        [MinLength(1, ErrorMessage = "Phải có ít nhất một địa chỉ.")]
        [MaxLength(3, ErrorMessage = "Chỉ được phép nhập tối đa 3 địa chỉ.")]
        public List<AddressModel> Address { get; set; } = new List<AddressModel>();

        public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, ResponseObject>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ICustomerRepository _customerRepository;

            public UpdateCustomerAddressCommandHandler(IHttpContextAccessor httpContextAccessor, ICustomerRepository customerRepository)
            {
                _httpContextAccessor = httpContextAccessor;
                _customerRepository = customerRepository;
            }

            public async Task<ResponseObject> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserId = context.GetName();

                string jsonString = JsonSerializer.Serialize(request.Address);

                object param = new
                {
                    UserId,
                    Address = jsonString
                };

                var result = await _customerRepository.spCustomerUpdateAddress(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Khách hàng không tồn tại.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Sửa địa chỉ người dùng thành công.";
                    }

                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Cập nhật thông tin thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
