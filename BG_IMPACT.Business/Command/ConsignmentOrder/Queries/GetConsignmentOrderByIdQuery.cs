﻿using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.BookList.Queries
{
    public class GetConsignmentOrderByIdQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid ConsignmentOrderId { get; set; }

        public class GetConsignmentOrderByIdHandler : IRequestHandler<GetConsignmentOrderByIdQuery, ResponseObject>
        {
            private readonly IConsignmentOrderRepository _consignmentOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetConsignmentOrderByIdHandler(IConsignmentOrderRepository consignmentOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _consignmentOrderRepository = consignmentOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetConsignmentOrderByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.ConsignmentOrderId,

                };

                var result = await _consignmentOrderRepository.spConsignmentOrderGetById(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && dict.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy vật phẩm.";
                }

                return response;
            }
        }
    }
}
