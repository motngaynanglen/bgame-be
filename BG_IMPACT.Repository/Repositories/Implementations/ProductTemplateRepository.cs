using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class ProductTemplateRepository : IProductTemplateRepository
    {
        private readonly SqlConnection _connection;

        public ProductTemplateRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object?> spProductTemplateCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spProductTemplateCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spProductTemplateGetListByGroupRefID(object param)
        {
            object? result = await _connection.QueryAsync("spProductTemplateGetListByGroupRefID", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spProductTemplateGetListByStoreID(object param)
        {
            object? result = await _connection.QueryAsync("spProductTemplateGetListByStoreID", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spProductTemplateGetRentalsByStoreID(object param)
        {
            object? result = await _connection.QueryAsync("spProductTemplateGetRentalsByStoreID", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spProductTemplateUpdate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spProductTemplateUpdate", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        
    }
}
