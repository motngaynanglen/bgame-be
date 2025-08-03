using BG_IMPACT.Business.Command.Order.Commands;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.SupplyOrder.Commands
{
    public class SupplyItem
    {
        public string Name { get; set; } = string.Empty;
        public long Quantity { get; set; } 
    }

    public class CreateSupplyOrderCommand : IRequest<ResponseObject>
    {
        public Guid? StoreId { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public List<SupplyItem> SupplyOrders { get; set; } = new();
        
        public class CreateSupplyOrderCommandHandler : IRequestHandler<CreateSupplyOrderCommand, ResponseObject>
        {
            private readonly ISupplyOrderRepository _supplyOrderRepository;
            private readonly IStoreRepository _storeRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public CreateSupplyOrderCommandHandler(ISupplyOrderRepository supplyOrderRepository, IStoreRepository storeRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplyOrderRepository = supplyOrderRepository;
                _storeRepository = storeRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(CreateSupplyOrderCommand request, CancellationToken cancellationToken)
            {
                var response = new ResponseObject();

                var context = _httpContextAccessor.HttpContext;
                string? UserId = string.Empty;

                if (context != null && (context.GetRole() == "ADMIN" || context.GetRole() == "MANAGER"))
                {
                    UserId = context.GetName();
                }
                else
                {
                    response.StatusCode = "403";
                    response.Message = "Bạn không có quyền thực hiện thao tác này.";
                    return response;
                }

                if (context.GetRole() == "ADMIN" && request.StoreId == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Bạn chưa nhập cửa hàng. Xin hãy bổ sung thông tin còn thiếu.";
                    return response;
                }

                object param = new
                {
                    StoreId = request.StoreId,
                    Title = request.Title,
                    SupplierId = request.SupplierId,
                    UserId = UserId,
                    SupplyItems = ConvertToDataTable(request.SupplyOrders).AsTableValuedParameter("SupplyItemInputType")
                };

                var result = await _supplyOrderRepository.spSupplyOrderCreate(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && dict.ContainsKey("Status"))
                {
                    response.StatusCode = "200";
                    response.Message = dict["Message"].ToString() ?? "Tạo phiếu nhập thành công.";
                    response.Data = dict.ContainsKey("SupplyOrderId") ? dict["SupplyOrderId"] : null;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Tạo phiếu nhập thất bại. Vui lòng thử lại sau.";
                }

                return response;
            }

            private static DataTable ConvertToDataTable(List<SupplyItem> items)
            {
                var table = new DataTable();
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("quantity", typeof(string)); // vì trong SQL là nchar(10)

                foreach (var item in items)
                {
                    table.Rows.Add(item.Name, item.Quantity);
                }

                return table;
            }

        }
    }
}
