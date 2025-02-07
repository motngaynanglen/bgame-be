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

        public async Task<object> spStoreChangeStatus(object param)
        {
            object result = await _connection.ExecuteAsync("spStoreChangeStatus", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spStoreCreate(object param)
        {
            object result = await _connection.ExecuteAsync("spStoreCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public Task<object> spStoreGetById(object param)
        {
            throw new NotImplementedException();
        }

        public Task<object> spStoreGetList(object param)
        {
            throw new NotImplementedException();
        }

        public Task<object> spStoreUpdate(object param)
        {
            throw new NotImplementedException();
        }
    }
}
