using BG_IMPACT.DTO.Models.PagingModels;

namespace BG_IMPACT.Business.Command.Account.Queries
{
    public class GetAccountListByManagerQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; }

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

                object parameters = new
                {
                    UserID,
                    Role,
                    request.Paging.PageNum,
                    request.Paging.PageSize

                };

                object param2 = new
                {
                    UserID,
                    Role
                };


                long count = 0;

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
                var result = await _accountRepository.spAccountListGetByManager(parameters);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _accountRepository.spAccountListGetByManagerPageData(param2);
                var dict = pageData as IDictionary<string, object>;
                if (dict != null && Int64.TryParse(dict["TotalRows"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["TotalRows"].ToString(), out count);
                }
                if (list.Count > 0)
                {
                    long pageCount = count / request.Paging.PageSize;

                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                    response.Paging = new PagingModel
                    {
                        PageNum = request.Paging.PageNum,
                        PageSize = request.Paging.PageSize,
                        PageCount = count % request.Paging.PageSize == 0 ? pageCount : pageCount + 1
                    };
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

