using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.StoreTable.Commands
{
    public class CreateStoreTableCommand : IRequest<ResponseObject>
    {
        public Guid? StoreId { get; set; } = null;
        public int Capacity { get; set; } = 2;
        public int Amount { get; set; } = 1;
        public class CreateStoreTableCommandHandler : IRequestHandler<CreateStoreTableCommand, ResponseObject>
        {
            public readonly IStoreTableRepository _storeTableRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateStoreTableCommandHandler(IStoreTableRepository storetableRepository, IHttpContextAccessor httpContextAccessor)
            {
                _storeTableRepository = storetableRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(CreateStoreTableCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;
                string? UserID = context?.GetName() ?? null;

                object param = new
                {
                    request.StoreId,
                    UserID,
                    request.Capacity,
                    request.Amount,
                };

                var result = await _storeTableRepository.spStoreTableCreate(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 0)
                    {
                        response.StatusCode = "200";
                        response.Message = "Tạo bàn thành công";
                    }
                    else
                    {
                        response.StatusCode = "404";
                        response.Message = "B+àn đã tồn tại";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Tạo bàn thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
