using Dapper;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SqlConnection _connection;

        public OrderRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public Task<object?> spOrderClaimRequest(object param)
        {
            throw new NotImplementedException();
        }

        public async Task<object?> spOrderCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderCreateByCustomer(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderCreateByCustomer", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderCreateByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderCreateByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderGetById(object param)
        {
            using var multi = await _connection.QueryMultipleAsync("spOrderGetById", param, commandType: CommandType.StoredProcedure);

            var orderGroups = (await multi.ReadAsync()).ToList();
            var orders = (await multi.ReadAsync()).ToList();
            var orderItems = (await multi.ReadAsync()).ToList();

            foreach (var order in orders)
            {
                var orderId = (Guid)order.order_id;
                var items = orderItems
                    .Where(i => (Guid)i.order_id == orderId)
                    .ToList();

                var dict = (IDictionary<string, object>)order;
                dict["items"] = items;
            }

            foreach (var group in orderGroups)
            {
                var groupId = (Guid)group.order_group_id;
                var groupOrders = orders
                    .Where(o => (Guid)o.order_group_id == groupId)
                    .ToList();

                var dict = (IDictionary<string, object>)group;
                dict["orders"] = groupOrders;
            }

            return orderGroups.FirstOrDefault();
        }


        public async Task<(object? orderGroups, int totalCount)> spOrderGetPaged(object param)
        {
            using var multi = await _connection.QueryMultipleAsync("spOrderGetPaged", param, commandType: CommandType.StoredProcedure);

            var orderGroups = (await multi.ReadAsync()).ToList(); 
            var orders = (await multi.ReadAsync()).ToList();         
            var orderItems = (await multi.ReadAsync()).ToList();      
            var totalCount = await multi.ReadFirstOrDefaultAsync<int>();

            foreach (var order in orders)
            {
                var orderId = (Guid)order.id;
                var items = orderItems
                    .Where(i => (Guid)i.order_id == orderId)
                    .ToList();

                var dict = (IDictionary<string, object>)order;
                dict["items"] = items;
            }

            foreach (var group in orderGroups)
            {
                var groupId = (Guid)group.id;
                var groupOrders = orders
                    .Where(o => (Guid)o.order_group_id == groupId)
                    .ToList();

                var dict = (IDictionary<string, object>)group;
                dict["orders"] = groupOrders;
            }

            return (orderGroups, totalCount);
        }
        

        public async Task<object?> spOrderGetUnclaim(object param)
        {
            object? result = await _connection.QueryAsync("spOrderGetUnclaim", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderGetUnclaimPageData(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderGetUnclaimPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderHistory(object param)
        {
            object? result = await _connection.QueryAsync("spOrderHistory", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderHistoryPageData(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderHistoryPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderUpdateDeliveryInfo(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateDeliveryInfo", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderUpdateStatusToPaid(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateStatusToPaid", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderUpdateStatusToPrepare(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateStatusToPrepare", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spOrderUpdateStatusToSending(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateStatusToSending", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spOrderUpdateStatusToSent(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateStatusToSent", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spOrderCancel(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderCancel", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderUpdateStatusToDelivered(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateStatusToDelivered", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderUpdateStatusToReceived(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateStatusToReceived", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderUpdateIsTransfered(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateIsTransfered", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderUpdateIsHub(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderUpdateIsHub", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
