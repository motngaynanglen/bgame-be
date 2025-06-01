using BG_IMPACT.Business.Command.Supplier.Queries;
using BG_IMPACT.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.News.Queries
{
    public class GetListNewsQuery : IRequest<ResponseObject>
    {

        public class GetListNewsQueryQueryHandler : IRequestHandler<GetListNewsQuery, ResponseObject>
        {
            private readonly INewsRepository _newsRepository;

            public GetListNewsQueryQueryHandler(INewsRepository newsRepository)
            {
                _newsRepository = newsRepository;
            }
            public async Task<ResponseObject> Handle(GetListNewsQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();


                var result = await _newsRepository.spNewsGetList();
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
                    response.Message = "Không tìm thấy bản tin nào.";
                }
                return response;
            }
        }
    }
}
