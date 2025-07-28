using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.BookTable.Queries
{
    public class GetBookTableByDateQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public DateTimeOffset BookDate { get; set; }

        public class GetBookTableByDateQueryHandler : IRequestHandler<GetBookTableByDateQuery, ResponseObject>
        {
            private readonly IBookTableRepository _bookTableRepository;

            public GetBookTableByDateQueryHandler(IBookTableRepository bookTableRepository)
            {
                _bookTableRepository = bookTableRepository;
            }
            public async Task<ResponseObject> Handle(GetBookTableByDateQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.StoreId,
                    request.BookDate,

                };

                var result = await _bookTableRepository.spBookTableGetStoreTimeTableByDate(param);
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
