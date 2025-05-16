using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.BookList.Queries
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
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;
                string? Role = context?.GetRole() ?? null;

                if (Role == null || Guid.TryParse(UserID, out _) == false)
                {
                    response.StatusCode = "404";
                    response.Message = "Token trả về không đúng.";
                }
                else
                {
                    object param = new
                    {
                        UserID,
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
                }

                return response;
            }
        }
    }
}
