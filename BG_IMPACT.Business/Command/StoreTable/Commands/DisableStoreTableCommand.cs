using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.StoreTable.Commands
{
    public class DisableStoreTableCommand : IRequest<ResponseObject>
    {
        public Guid? Id { get; set; } = null;
        public class DisableStoreTableCommandHandler : IRequestHandler<DisableStoreTableCommand, ResponseObject>
        {
            public readonly IStoreTableRepository _storeTableRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DisableStoreTableCommandHandler(IStoreTableRepository storetableRepository, IHttpContextAccessor httpContextAccessor)
            {
                _storeTableRepository = storetableRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(DisableStoreTableCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;
                string? UserID = context?.GetName() ?? null;

                object param = new
                {
                    request.Id,
                    UserID,
                };

                var result = await _storeTableRepository.spStoreTableCreate(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 0)
                    {
                        response.StatusCode = "200";
                        response.Message = "Hủy bàn thành công";
                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "Bàn không tồn tại hoặc đã bị hủy";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Thao tác thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
