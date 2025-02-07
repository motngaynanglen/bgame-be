using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class ProductGroupRefRepository : IProductGroupRefRepository
    {
        private readonly SqlConnection _connection;

        public ProductGroupRefRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object> spProductGroupRefCreate(object param)
        {
            object result = await _connection.ExecuteAsync("spProductGroupRefCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spProductGroupRefGetList(object param)
        {
            object result = await _connection.QueryAsync("spProductGroupRefGetList", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
