using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SqlConnection _connection;

        public StoreRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object?> spStoreChangeStatus(object param)
        {
            object? result = await _connection.ExecuteAsync("spStoreChangeStatus", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spStoreCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spStoreCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public Task<object?> spStoreGetById(object param)
        {
            throw new NotImplementedException();
        }

        public async Task<object?> spStoreGetByUserID(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spStoreGetByUserID", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spStoreGetList(object param)
        {
            object result = await _connection.QueryAsync("spStoreGetList", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spStoreGetListAndProductCountById(object param)
        {
            object result = await _connection.QueryAsync("spStoreGetListAndProductCountById", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spStoreGetListByGroupRefId(object param)
        {
            object result = await _connection.QueryAsync("spStoreGetListByGroupRefId", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public Task<object?> spStoreUpdate(object param)
        {
            throw new NotImplementedException();
        }
    }
}
