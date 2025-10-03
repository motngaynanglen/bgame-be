using Newtonsoft.Json;

namespace BG_IMPACT.Business.Command.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<ResponseObject>
    {
        public Guid OrderID { get; set; }
        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ResponseObject>
        {
            private readonly IOrderRepository _orderRepository;

            public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task<ResponseObject> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.OrderID,
                };

                var result = await _orderRepository.spOrderGetById(param);
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
                    response.Message = "Không tìm thấy đơn hàng.";
                }

                return response;
            }
        }

    }
}