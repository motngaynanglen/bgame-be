using BG_IMPACT.Business.Jobs;
using BG_IMPACT.Business.JobSchedulers.Jobs;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static BG_IMPACT.Repository.Repositories.Implementations.EmailServiceRepository;

namespace BG_IMPACT.Business.Command.Order.Commands
{
    public class OrderFormItem
    {
        public Guid StoreId { get; set; }
        public Guid ProductTemplateId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderByCustomerCommand : IRequest<ResponseObject>
    {
        [Required]
        public List<OrderFormItem> Orders { get; set; } = new();
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public class CreateOrderByCustomerCommandHandler : IRequestHandler<CreateOrderByCustomerCommand, ResponseObject>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IEmailServiceRepository _emailServiceRepository;
            private readonly IJobScheduler _jobScheduler;

            public CreateOrderByCustomerCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, IEmailServiceRepository emailServiceRepository, IJobScheduler jobScheduler)
            {
                _orderRepository = orderRepository;
                _httpContextAccessor = httpContextAccessor;
                _emailServiceRepository = emailServiceRepository;
                _jobScheduler = jobScheduler;
            }
            public async Task<ResponseObject> Handle(CreateOrderByCustomerCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;
                string? CustomerID = string.Empty;
                if (context != null && context.GetRole() == "CUSTOMER")
                {
                    _ = Guid.TryParse(context.GetName(), out Guid cusId);
                    CustomerID = cusId.ToString();
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Phương thức chỉ cho khách hàng sử dụng";
                    return response;
                }


                object param = new
                {
                    CustomerID,
                    request.Email,
                    request.FullName,
                    request.PhoneNumber,
                    request.Address,
                    OrdersCreateForm = ConvertToDataTable(request.Orders).AsTableValuedParameter("OrdersCreateFormItemType")
                };

                var result = await _orderRepository.spOrderCreateByCustomer(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);
                    var message = dict["Message"].ToString() ?? "";
                    if (count == 0)
                    {
                        response.StatusCode = "200";
                        response.Message = message;
                        response.Data = dict["Data"].ToString() ?? null;

                        object param3 = new
                        {
                            OrderId = response.Data
                        };

                        var result2 = await _orderRepository.spOrderGetById(param3);
                        var rawData = result2 as IDictionary<string, object>;
                        var order = JsonConvert.DeserializeObject<OrderGroupModel>(rawData["json"] as string);

                        await _jobScheduler.ScheduleOnDemandJobAsync(
                            requestId: "Send email request: " + response.Data?.ToString(),
                            jobType: typeof(SendEmailJob),
                            delay: TimeSpan.FromSeconds(15),
                            id: response.Data?.ToString(),
                            payload: order
                        );

                        await _jobScheduler.ScheduleOnDemandJobAsync(
                            requestId:  "Delete order request: " + response.Data?.ToString(),
                            jobType: typeof(CancelOrderJob),
                            delay: TimeSpan.FromHours(1),
                            id: response.Data?.ToString(),
                            payload: null
                        );

                        return response;
                    }
                    else if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = message;
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "403";
                        response.Message = message;
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = message;
                    }
                    else if (count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = message;
                    }
                    else if (count == 9)
                    {
                        response.StatusCode = "500";
                        response.Message = message;
                    }
                }
                else
                {
                    response.StatusCode = "500";
                    response.Message = "Mua hàng thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
        private static DataTable ConvertToDataTable(List<OrderFormItem> orders)
        {
            var table = new DataTable();
            table.Columns.Add("StoreID", typeof(Guid));
            table.Columns.Add("ProductTemplateID", typeof(Guid));
            table.Columns.Add("Quantity", typeof(int));

            foreach (var item in orders)
            {
                table.Rows.Add(item.StoreId, item.ProductTemplateId, item.Quantity);
            }

            return table;
        }

        public class OrderGroupModel
        {
            public string order_id { get; set; }
            public string order_code { get; set; }
            public string email { get; set; }
            public string full_name { get; set; }
            public string phone_number { get; set; }
            public string address { get; set; }
            public int total_item { get; set; }
            public double total_price { get; set; }
            public string order_status { get; set; }
            public DateTime order_created_at { get; set; }
            public int is_delivery { get; set; }
            public List<OrderModel> orders { get; set; }
        }

        public class OrderModel
        {
            public string id { get; set; }
            public string code { get; set; }
            public int total_item { get; set; }
            public double total_price { get; set; }
            public string status { get; set; }
            public List<OrderItemModel> order_items { get; set; }
        }
    }
}
