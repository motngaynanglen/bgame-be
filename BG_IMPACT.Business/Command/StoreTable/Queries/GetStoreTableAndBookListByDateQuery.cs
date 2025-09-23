using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.StoreTable.Queries
{
    public class GetStoreTableAndBookListByDateQuery : IRequest<ResponseObject>
    {
        [Required]
        public DateTimeOffset Date { get; set; }

        public class GetStoreTableAndBookListByDateQueryHandler : IRequestHandler<GetStoreTableAndBookListByDateQuery, ResponseObject>
        {
            private readonly IStoreTableRepository _storeTableRepository;

            private readonly IHttpContextAccessor _httpContextAccessor;
            public GetStoreTableAndBookListByDateQueryHandler(IStoreTableRepository storeTableRepository, IHttpContextAccessor httpContextAccessor)
            {
                _storeTableRepository = storeTableRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(GetStoreTableAndBookListByDateQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserId = null;
                string? Role = null;

                if (context != null)
                {
                    UserId = context.GetName();
                    Role = context.GetRole();
                }

                object param = new
                {
                    request.Date,
                    UserId,
                    Role
                };

                var result = await _storeTableRepository.spStoreTableGetBookListByDate(param);
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
                    response.Message = "Không tìm thấy đơn hàng nào.";
                }

                return response;
            }
        }
    }
}
