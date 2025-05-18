using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.OrderItem.Commands
{
    public class UpdateOrderItemProductCommand : IRequest<ResponseObject>
    {
        // public Guid StaffId { get; set; }
        [Required]
        public Guid OrderItemId { get; set; }
        [Required]
        public Guid ProductId { get; set; }

        public class UpdateOrderItemProductCommandHandler : IRequestHandler<UpdateOrderItemProductCommand, ResponseObject>
        {
            private readonly IOrderItemRepository _OrderItemRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateOrderItemProductCommandHandler(IOrderItemRepository OrderItemRepository, IHttpContextAccessor httpContextAccessor)
            {
                _OrderItemRepository = OrderItemRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(UpdateOrderItemProductCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? StaffId = null;

                if (context != null && context.GetRole() == "STAFF")
                {
                    StaffId = context.GetName();
                }
                // code param và dọi database ở đây
                object param = new
                {
                    StaffId,
                    request.OrderItemId,
                    request.ProductId,
                };

                var result = await _OrderItemRepository.spOrderItemUpdateProduct(param);
                var dict = result as IDictionary<string, object>;

                // return format: Status, Message, Data
                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);
                    string? Message = dict["Message"].ToString() ?? string.Empty; // không có thông báo
                    string? Data = dict["Data"].ToString() ?? null;

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "403";
                        response.Message = Message;
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = Message;
                        response.Data = Data;
                    }
                }
                return response;
            }
        }
    }
}