using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.BookList.Queries
{
    public class GetBookListTimeTableByDateQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public DateTimeOffset BookDate { get; set; }
        [Required]
        public List<Guid> ProductTemplateIds { get; set; } = new List<Guid>();

        public class GetBookListTimeTableByDateQueryHandler : IRequestHandler<GetBookListTimeTableByDateQuery, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetBookListTimeTableByDateQueryHandler(IBookListRepository bookListRepository, IHttpContextAccessor httpContextAccessor)
            {
                _bookListRepository = bookListRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetBookListTimeTableByDateQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;
                string? UserId = null;

                if (context != null)
                {
                    UserId = context.GetName();
                }
                string ProductTemplateIDListString = string.Join(",", request.ProductTemplateIds);
                object param = new
                {
                    request.StoreId,
                    request.BookDate,
                    UserId,
                    ProductTemplateIDListString,
                };

                var result = await _bookListRepository.spBookListGetStoreTimeTableByDate(param);
                if (result == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Có gì đó sai sai (lỗi result trả về 'vô giá trị')";
                    return response;
                }
                var list = ((IEnumerable<dynamic>)result).ToList();

                if (list.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "200";
                    response.Message = "Cửa hàng không tồn tại bàn";
                }


                return response;
            }
        }
    }
}
