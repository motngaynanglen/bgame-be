using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class ConsignmentOrderRepository : IConsignmentOrderRepository
    {
        private readonly SqlConnection _connection;

        public ConsignmentOrderRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object?> spConsignmentOrderGetById(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spConsignmentOrderGetById", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spConsignmentOrderGetList(object param)
        {
            object? result = await _connection.QueryAsync("spConsignmentOrderGetList", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spConsignmentOrderCancelByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spConsignmentOrderCancelByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spConsignmentOrderCreateByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spConsignmentOrderCreateByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spConsignmentOrderUpdateByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spConsignmentOrderUpdateByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
