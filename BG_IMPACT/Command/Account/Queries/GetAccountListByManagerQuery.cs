using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Account.Queries
{
    public class GetAccountListByManagerQuery : IRequest<ResponseObject>
    {
        public class GetAccountListByManagerQueryQueryHandler : IRequestHandler<GetAccountListByManagerQuery, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;


            public GetAccountListByManagerQueryQueryHandler(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
            {
                _accountRepository = accountRepository;
                _httpContextAccessor = httpContextAccessor;

            }
            public async Task<ResponseObject> Handle(GetAccountListByManagerQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? string.Empty;
                string? Role = context?.GetRole() ?? string.Empty;

                if (Role != "MANAGER")
                {
                    response.StatusCode = "403"; // Forbidden
                    response.Message = "Bạn không có quyền truy cập vào dữ liệu này.";
                    return response;
                }

                // Kiểm tra nếu UserID không hợp lệ
                if (string.IsNullOrEmpty(UserID) || Guid.TryParse(UserID, out _) == false)
                {
                    response.StatusCode = "404";
                    response.Message = "Token trả về không đúng.";
                    return response;
                }

                // Tiến hành lấy danh sách các account nếu là Manager
                var result = await _accountRepository.spAccountListGetByManager();
                var list = ((IEnumerable<dynamic>)result).ToList();

                if (list.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy account nào.";
                }

                return response;
            }
        }
    }
}

