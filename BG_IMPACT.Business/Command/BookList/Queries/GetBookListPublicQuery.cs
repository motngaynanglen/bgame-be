using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.BookList.Queries
{
    public class GetBookListPublicQuery : IRequest<ResponseObject>
    {
        public Guid? ProductTemplateId { get; set; }
        public Paging Paging { get; set; } = new Paging();

        public class GetBookListPublicQueryHandler : IRequestHandler<GetBookListPublicQuery, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;

            public GetBookListPublicQueryHandler(IBookListRepository bookListRepository)
            {
                _bookListRepository = bookListRepository;
            }

            public async Task<ResponseObject> Handle(GetBookListPublicQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.ProductTemplateId,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                var result = await _bookListRepository.spBookListGetPublic(param);
                var list = ((IEnumerable<dynamic>)result.bookLists).ToList();
                long count = result.totalCount;

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
                    response.Message = "Không tìm thấy đơn thuê nào.";
                }

                return response;
            }
        }
    }
}
