namespace BG_IMPACT.Business.Command.Account.Commands
{
    public class UpdateStaffProfileCommand : IRequest<ResponseObject>
    {
        public Guid? AccountID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid? UserId { get; set; }


        public class UpdateStaffProfileCommandCommandHandler : IRequestHandler<UpdateStaffProfileCommand, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;

            private readonly IHttpContextAccessor _httpContextAccessor;


            public UpdateStaffProfileCommandCommandHandler(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
            {
                _accountRepository = accountRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(UpdateStaffProfileCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? string.Empty;
                string? GenderStr = string.Empty;

                if (request.Gender == Gender.FEMALE) GenderStr = "FEMALE";
                if (request.Gender == Gender.MALE) GenderStr = "MALE";
                //var genderStr = request.Gender.ToString().ToUpper(); 
                object parameters = new
                {
                    request.AccountID,
                    request.FullName,
                    request.DateOfBirth,
                    GenderStr,
                    request.Image,
                    request.Email,
                    request.PhoneNumber,
                    UserID

                };
                var result = await _accountRepository.spUpdateStaffProfile(parameters);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Account không tồn tại.";
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Manager không có quyền cập nhật thông tin staff này.";
                    }

                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Sửa thông tin Staff thành công.";
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
