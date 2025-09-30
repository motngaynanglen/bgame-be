using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Interfaces
{
    public class BookItemRepository : IBookItemRepository
    {
        private readonly SqlConnection _connection;

        public BookItemRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object?> spBookItemUpdateProduct(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookItemUpdateProduct", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
