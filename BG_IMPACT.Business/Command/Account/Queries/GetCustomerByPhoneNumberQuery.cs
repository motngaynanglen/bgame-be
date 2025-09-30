using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Account.Queries
{
    public class GetCustomerByPhoneNumberQuery : IRequest<ResponseObject>
    {
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chỉ được chứa ký tự số.")]
        [MinLength(1, ErrorMessage = "Số điện thoại phải có ít nhất 1 chữ số.")]
        [MaxLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 chữ số.")]
        public string PhoneNumber { get; set; } = string.Empty;

        public class GetCustomerByPhoneNumberQueryHandler : IRequestHandler<GetCustomerByPhoneNumberQuery, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;

            public GetCustomerByPhoneNumberQueryHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }

            public async Task<ResponseObject> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    PhoneNumber = request.PhoneNumber
                };

                var result = await _accountRepository.spCustomerGetByPhoneNumber(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                if (list != null && list.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy thông tin người dùng.";
                }

                return response;
            }
        }
    }
}
