namespace BG_IMPACT.Business.Command.Account.Commands
{
    public class UpdateCustomerProfileCommand : IRequest<ResponseObject>
    {
        public Guid? PersonID { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTimeOffset DateOfBirth { get; set; }
        public string Image { get; set; } = string.Empty;
        public Gender Gender { get; set; }


    }
    public class UpdateCustomerProfileCommandHandler : IRequestHandler<UpdateCustomerProfileCommand, ResponseObject>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCustomerProfileCommandHandler(ICustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            ResponseObject response = new();

            var context = _httpContextAccessor.HttpContext;

            string? UserID = context?.GetName() ?? string.Empty;
            string? Role = context?.GetRole() ?? string.Empty;
            string? GenderStr = string.Empty;

            if (request.Gender == Gender.FEMALE) GenderStr = "FEMALE";
            if (request.Gender == Gender.MALE) GenderStr = "MALE";
            //var genderStr = request.Gender.ToString().ToUpper(); 
            object parameters = new
            {
                request.PersonID,
                request.PhoneNumber,
                request.Email,
                request.FullName,
                request.DateOfBirth,
                request.Image,
                GenderStr,
                UserID,
                Role

            };
            var result = await _customerRepository.spCustomerUpdateProfile(parameters);
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
                    response.Message = "Sửa thông tin người dùng thành công.";
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
