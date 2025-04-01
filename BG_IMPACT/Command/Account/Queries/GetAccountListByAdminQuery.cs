using BG_IMPACT.Command.BookList.Queries;
using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Account.Queries
{
    public class GetAccountListByAdminQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; } = new Paging();
        public class GetAccountListByAdminQueryHandler : IRequestHandler<GetAccountListByAdminQuery, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;
            public GetAccountListByAdminQueryHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }
            public async Task<ResponseObject> Handle(GetAccountListByAdminQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var result = await _accountRepository.spAccountListGetByAdmin();
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
