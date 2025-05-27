namespace BG_IMPACT.Business.Command.Account.Queries
{
    public class GetAccountProfileQuery : IRequest<ResponseObject>
    {
        public class GetAccountProfileQueryHandler : IRequestHandler<GetAccountProfileQuery, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetAccountProfileQueryHandler(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
            {
                _accountRepository = accountRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(GetAccountProfileQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;
                string? Role = context?.GetRole() ?? null;

                if (Role == null || Guid.TryParse(UserID, out _) == false)
                {
                    response.StatusCode = "404";
                    response.Message = "Token trả về không đúng.";
                }
                else
                {
                    object param = new
                    {
                        UserID,
                        Role
                    };

                    var result = await _accountRepository.spAccountGetProfile(param);
                    var dict = result as IDictionary<string, object>;

                    if (dict != null && dict.Count > 0)
                    {
                        response.StatusCode = "200";
                        response.Data = dict;
                        response.Message = string.Empty;
                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Không tìm thấy thông tin người dùng.";
                    }

                }
                return response;
            }
        }

    }
}
