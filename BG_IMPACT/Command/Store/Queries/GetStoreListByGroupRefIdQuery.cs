using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Store.Queries
{
    public class GetStoreListByGroupRefIdQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid GroupRefId { get; set; }
        [Required]
        public Paging Paging { get; set; } = new();

        public class GetStoreListByGroupRefIdQueryHandler : IRequestHandler<GetStoreListByGroupRefIdQuery, ResponseObject>
        {
            private readonly IStoreRepository _storeRepository;

            public GetStoreListByGroupRefIdQueryHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }
            public async Task<ResponseObject> Handle(GetStoreListByGroupRefIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.GroupRefId                };

                var result = await _storeRepository.spStoreGetListByGroupRefId(param);
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
                    response.Message = "Không tìm thấy cửa hàng nào.";
                }

                return response;
            }
        }
    }
}
