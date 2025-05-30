﻿using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.Store.Queries
{
    public class GetStoreListAndProductCountByIdQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid ProductTemplateId { get; set; }

        public class GetStoreListAndProductCountByIdQueryHandler : IRequestHandler<GetStoreListAndProductCountByIdQuery, ResponseObject>
        {
            private readonly IStoreRepository _storeRepository;

            public GetStoreListAndProductCountByIdQueryHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }
            public async Task<ResponseObject> Handle(GetStoreListAndProductCountByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.ProductTemplateId
                };

                var result = await _storeRepository.spStoreGetListAndProductCountById(param);

                if (result == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy cửa hàng nào.";
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
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy cửa hàng nào.";
                }

                return response;
            }
        }
    }
}
