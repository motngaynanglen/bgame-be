using BG_IMPACT.Repositories.Interfaces;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repository.Repositories.Implementations
{
    public class SupplyOrderRepository : ISupplyOrderRepository
    {
        private readonly SqlConnection _connection;

        public SupplyOrderRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        
        public async Task<object> spSupplyOrderCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spSupplyOrderCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spEmailGetSupplierEmailByOrderId(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spEmailGetSupplierEmailByOrderId", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spGetSupplyItemsByOrderId(object param)
        {
            object? result = await _connection.QueryAsync("spGetSupplyItemsByOrderId", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<(object? supplyOrders, int totalCount)> spSupplyOrderGetList(object param)
        {
            using var multi = await _connection.QueryMultipleAsync("spSupplyOrderGetList", param, commandType: CommandType.StoredProcedure);

            var supplyOrders = (await multi.ReadAsync()).ToList();

            var totalCount = await multi.ReadFirstOrDefaultAsync<int>();

            return (supplyOrders, totalCount);
        }

        public async Task<object?> spSupplyOrderGetById(object param)
        {
            using var multi = await _connection.QueryMultipleAsync("spSupplyOrderGetById", param, commandType: CommandType.StoredProcedure);

            var supplyOrder = await multi.ReadFirstOrDefaultAsync();
            if (supplyOrder == null)
                return null;

            var items = (await multi.ReadAsync()).ToList();

            var dict = (IDictionary<string, object>)supplyOrder;
            dict["items"] = items;

            return supplyOrder;
        }
    }
}
