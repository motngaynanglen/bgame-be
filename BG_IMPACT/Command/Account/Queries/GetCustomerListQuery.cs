using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BG_IMPACT.Command.Account.Queries
{
    public class GetCustomerListQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; } = new Paging();
        public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, ResponseObject>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetCustomerListQueryHandler(ICustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
            {
                _customerRepository = customerRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
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
                    var result = await _customerRepository.spCustomerGetList(param);
                    var list = ((IEnumerable<dynamic>)result).ToList();

                    var pageData = await _customerRepository.spCustomerGetList(param2);
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
                        response.Message = "Không tìm Customer nào.";
                    }
                }
                return response;
                }
            }

        }
    }
