using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.BookList.Queries
{
    public class GetConsignmentOrderQuery : IRequest<ResponseObject>
    {

        public Paging Paging { get; set; } = new Paging();

        public class GetConsignmentOrderHandler : IRequestHandler<GetConsignmentOrderQuery, ResponseObject>
        {
            private readonly IConsignmentOrderRepository _consignmentOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetConsignmentOrderHandler(IConsignmentOrderRepository consignmentOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _consignmentOrderRepository = consignmentOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetConsignmentOrderQuery request, CancellationToken cancellationToken)
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

                    var result = await _consignmentOrderRepository.spConsignmentOrderGetList(param);
                    var list = ((IEnumerable<dynamic>)result).ToList();

                    //var pageData = await _bookListRepository.spBookListGetPageData(param2);
                    //var dict = pageData as IDictionary<string, object>;
                    long count = 0;

                    //if (dict != null && Int64.TryParse(dict["TotalRows"].ToString(), out _) == true)
                    //{
                    //    _ = Int64.TryParse(dict["TotalRows"].ToString(), out count);
                    //}

                    if (list.Count > 0)
                    {
                        //long pageCount = count / request.Paging.PageSize;

                        response.StatusCode = "200";
                        response.Data = list;
                        response.Message = string.Empty;
                        //response.Paging = new PagingModel
                        //{
                        //    PageNum = request.Paging.PageNum,
                        //    PageSize = request.Paging.PageSize,
                        //    PageCount = count % request.Paging.PageSize == 0 ? pageCount : pageCount + 1
                        //};
                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Không tìm thấy đơn thuê nào.";
                    }
                }

                return response;
            }
        }
    }
}
