﻿using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Login.Commands
{
    public class CreateStaffCommand : IRequest<ResponseObject>
    {

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Tên tài khoản phải từ 8 đến 20 ký tự")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 đến 20 ký tự")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Tên người dùng phải từ 4 đến 50 ký tự")]
        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
        public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, ResponseObject>
        {
            public readonly IAccountRepository _accountRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateStaffCommandHandler(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
            {
                _accountRepository = accountRepository;
                _httpContextAccessor = httpContextAccessor;

            }
            public async Task<ResponseObject> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;
                string? ManagerID = null;
                ManagerID = context.GetName();

                object param = new
                {
                    ManagerID,
                    username = request.Username,
                    password = request.Password,
                    phone_number = request.PhoneNumber ?? string.Empty,
                    email = request.Email ?? string.Empty,
                    role = "STAFF",
                    full_name = request.FullName,
                    date_of_birth = request.DateOfBirth,
                };

                var result = await _accountRepository.spAccountCreateStaff(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 0)
                    {
                        response.StatusCode = "200";
                        response.Message = "Tạo tài khoản thành công";
                    }
                    else if (count == 1)
                    {
                        response.StatusCode = "400";
                        response.Message = "Tên tài khoản đã tồn tại";
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Cửa hàng không tồn tại";
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không tìm thấy thông tin người quản lý";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Tạo tài khoản thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
