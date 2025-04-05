using Dapper;
using BG_IMPACT.Repositories.Interfaces;
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

        public async Task<object?> spOrderCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spOrderCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
