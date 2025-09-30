using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using BG_IMPACT.Repository.Repositories.Interfaces;

namespace BG_IMPACT.Repositories.Interfaces
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SqlConnection _connection;

        public OrderItemRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object?> spOrderItemGetItemsByOrderIds(object param)
        {
            object? result = await _connection.QueryAsync("spOrderItemGetItemsByOrderIds", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spOrderItemUpdateProduct(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderItemUpdateProduct", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
