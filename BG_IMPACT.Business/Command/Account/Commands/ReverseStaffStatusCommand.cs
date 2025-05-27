using BG_IMPACT.Infrastructure.Extensions;

namespace BG_IMPACT.Business.Command.Account.Commands
{
    public class ReverseStaffStatusCommand : IRequest<ResponseObject>
    {
        public Guid? AccountID { get; set; }

        public class ReverseStaffStatusCommandCommandHandler : IRequestHandler<ReverseStaffStatusCommand, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;

            private readonly IHttpContextAccessor _httpContextAccessor;


            public ReverseStaffStatusCommandCommandHandler(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
            {
                _accountRepository = accountRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(ReverseStaffStatusCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? string.Empty;
                object parameters = new
                {
                    request.AccountID,
                    UserID

                };
                var result = await _accountRepository.spAccountReverseStaffStatus(parameters);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    string? Message = dict["Message"].ToString() ?? string.Empty;

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }
                    else if (count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }

                    else
                    {
                        response.StatusCode = "200";
                        response.Message = Message;
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
