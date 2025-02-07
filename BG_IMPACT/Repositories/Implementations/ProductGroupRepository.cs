using BG_IMPACT.Repositories.Interfaces;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace BG_IMPACT.Repositories.Implementations
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly SqlConnection _connection;

        public ProductGroupRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object> spProductGroupCreate(object param)
        {
            object? result = await _connection.ExecuteAsync("spProductGroupCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spProductGroupGetList()
        {
            object? result = await _connection.QueryAsync("spProductGroupGetList", null, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
