using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class StoreTableRepository : IStoreTableRepository
    {
        private readonly SqlConnection _connection;

        public StoreTableRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object?> spStoreTableCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spStoreTableCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spStoreTableDisableById(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spStoreTableDisableById", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spStoreTableUpdate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spStoreTableUpdate", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spStoreTableGetListByStoreID(object param)
        {
            object? result = await _connection.QueryAsync("spStoreTableGetListByStoreID", param, commandType: CommandType.StoredProcedure);
            return result;
        }

       
    }
}
