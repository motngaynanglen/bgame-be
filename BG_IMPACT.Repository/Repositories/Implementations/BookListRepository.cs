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
        public async Task<object?> spBookListCreateByCustomer(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListCreateByCustomer", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListCreateByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListCreateByStaff", param, commandType: CommandType.StoredProcedure);
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

        public async Task<object?> spBookListGetAvailableProduct(object param)
        {
            object? result = await _connection.QueryAsync("spBookListGetAvailableProduct", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListGetAvailableSlot(object param)
        {
            object? result = await _connection.QueryAsync("spBookListGetAvailableSlot", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListGetById(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListGetById", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<(object? bookLists, int totalCount)> spBookListGetPaged(object param)
        {
            using var multi = await _connection.QueryMultipleAsync("spBookListGetPaged", param, commandType: CommandType.StoredProcedure);

            var bookLists = (await multi.ReadAsync()).ToList();     
            var products = (await multi.ReadAsync()).ToList();   
            var totalCount = await multi.ReadFirstOrDefaultAsync<int>();

            foreach (var bookList in bookLists)
            {
                var bookListId = (Guid)bookList.id; 
                var bookListProducts = products
                    .Where(p => (Guid)p.id == bookListId) 
                    .ToList();

                var dict = ((IDictionary<string, object>)bookList);
                dict["products"] = bookListProducts;
            }

            return (bookLists, totalCount);
        }

        public async Task<object?> spBookListGetPageData(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListGetPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListHistory(object param)
        {
            object? result = await _connection.QueryAsync("spBookListHistory", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListHistoryPageData(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListHistoryPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spBookListStart(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookListStart", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
