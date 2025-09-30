using BG_IMPACT.Repository.Repositories.Interfaces;
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

        public async Task<object?> spStoreTableGetBookListByDate(object param)
        {
            using var multi = await _connection.QueryMultipleAsync(
                "spStoreTableGetBookListByDate",
                param,
                commandType: CommandType.StoredProcedure);

            var storeTables = (await multi.ReadAsync()).ToList();
            var bookTables = (await multi.ReadAsync()).ToList();
            var bookLists = (await multi.ReadAsync()).ToList();
            var products = (await multi.ReadAsync()).ToList();

            foreach (var bookList in bookLists)
            {
                var bookListId = (Guid)((IDictionary<string, object>)bookList)["id"];
                var listProducts = products
                    .Where(p =>
                    {
                        var dict = (IDictionary<string, object>)p;
                        return dict.ContainsKey("book_list_id")
                               && dict["book_list_id"] != null
                               && Guid.TryParse(dict["book_list_id"].ToString(), out var guid)
                               && guid == bookListId;
                    })
                    .ToList();

                ((IDictionary<string, object>)bookList)["products"] = listProducts;
            }

            foreach (var bookTable in bookTables)
            {
                var bookTableId = (Guid)((IDictionary<string, object>)bookTable)["id"];
                var lists = bookLists
                    .Where(bl => (Guid)((IDictionary<string, object>)bl)["book_table_id"] == bookTableId)
                    .ToList();

                ((IDictionary<string, object>)bookTable)["bookLists"] = lists;
            }

            foreach (var storeTable in storeTables)
            {
                var storeTableId = (Guid)((IDictionary<string, object>)storeTable)["TableID"];

                var relatedBookTables = bookTables
                    .Where(bt =>
                    {
                        var dict = (IDictionary<string, object>)bt;
                        if (dict.TryGetValue("table_list_ids", out var rawIds) && rawIds is string idsStr)
                        {
                            var ids = idsStr.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                            .Select(id => Guid.Parse(id.Trim()))
                                            .ToList();

                            return ids.Contains(storeTableId);
                        }
                        return false;
                    })
                    .ToList();

                ((IDictionary<string, object>)storeTable)["bookTables"] = relatedBookTables;
            }

            return storeTables;
        }
    }
}
