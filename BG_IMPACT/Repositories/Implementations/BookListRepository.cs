using BG_IMPACT.Repositories.Interfaces;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace BG_IMPACT.Repositories.Implementations
{
    public class BookListRepository : IBookListRepository
    {
        private readonly SqlConnection _connection;

        public BookListRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object?> spBookListCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListEnd(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListEnd", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListExtend(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListExtend", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListGet(object param)
        {
            object? result = await _connection.QueryAsync("spBookListGet", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListGetPageData(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListGetPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListStart(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListStart", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
