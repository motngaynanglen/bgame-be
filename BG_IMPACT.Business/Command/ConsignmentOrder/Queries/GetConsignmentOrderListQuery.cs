using BG_IMPACT.DTO.Models.PagingModels;

namespace BG_IMPACT.Business.Command.BookList.Queries
{
    public class GetConsignmentOrderListQuery : IRequest<ResponseObject>
    {

        public Paging Paging { get; set; } = new Paging();

        public class Handler : IRequestHandler<GetConsignmentOrderListQuery, ResponseObject>
        {
            private readonly IConsignmentOrderRepository _consignmentOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IConsignmentOrderRepository consignmentOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _consignmentOrderRepository = consignmentOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetConsignmentOrderListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                //string? UserID = context?.GetName() ?? null;
                //string? Role = context?.GetRole() ?? null;

                //if (Role == null || Guid.TryParse(UserID, out _) == false)
                //{
                //    response.StatusCode = "404";
                //    response.Message = "Token trả về không đúng.";
                //}
                //else
                //{
                //object param = new
                //{
                //    UserID,
                //    Role,
                //    request.Paging.PageNum,
                //    request.Paging.PageSize
                //};
                //var UserID = "";
                object param = new
                {
                    //UserID,

                };

                var result = await _consignmentOrderRepository.spConsignmentOrderGetList(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                //var pageData = await _bookListRepository.spBookListGetPageData(param2);
                //var dict = pageData as IDictionary<string, object>;
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
                //}

                return response;
            }
        }
    }
}
