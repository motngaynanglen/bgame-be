using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;

        public ProductRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object> spProductCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spProductCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spProductCreateTemplate(object param)
        {
            object? result = await _connection.ExecuteAsync("spProductCreateTemplate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spProductGetList(object param)
        {
            object? result = await _connection.QueryAsync("spProductGetList", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spProductCreateUnknown(object param)
        {
            object? result = await _connection.QueryAsync("spProductCreateUnknown", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spProductGetListByStoreId(object param)
        {
            object? result = await _connection.QueryAsync("spProductGetListByStoreId", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
