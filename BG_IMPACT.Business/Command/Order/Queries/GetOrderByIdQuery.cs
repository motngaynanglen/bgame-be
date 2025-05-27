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
                var rawData = result as IDictionary<string, object>;
                var order = JsonConvert.DeserializeObject<OrderDto>(rawData["json"] as string);
                var dict = order;

                if (dict != null)//&& dict.Count > 0
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
            private class OrderDto
            {
                public Guid order_id { get; set; }
                public string order_code { get; set; }
                public string email { get; set; }
                public string full_name { get; set; }
                public string phone_number { get; set; }
                public string address { get; set; }
                public int total_item { get; set; }
                public double total_price { get; set; }
                public string order_status { get; set; }
                public DateTime order_created_at { get; set; }
                public List<OrderItemDto> order_items { get; set; }
            }

            private class OrderItemDto
            {
                public Guid order_item_id { get; set; }
                public Guid? product_id { get; set; }
                public double current_price { get; set; }
                public string order_item_status { get; set; }
                public DateTime order_item_created_at { get; set; }
                public Guid product_template_id { get; set; }
                public string template_product_name { get; set; }
                public string template_image { get; set; }
                public double template_price { get; set; }
                public double template_rent_price { get; set; }
                public string template_description { get; set; }
            }
        }

    }
}