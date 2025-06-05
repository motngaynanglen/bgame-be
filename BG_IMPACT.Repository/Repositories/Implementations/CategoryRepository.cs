using BG_IMPACT.Repositories.Interfaces;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using BG_IMPACT.Repository.Repositories.Interfaces;

namespace BG_IMPACT.Repository.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object> spCategoryCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spCategoryCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spCategoryUpdate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spCategoryUpdate", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spCategoryGetList()
        {
            object? result = await _connection.QueryAsync("spCategoryGetList", commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spCategoryGetListByAdmin(object param)
        {
            object? result = await _connection.QueryAsync("spCategoryGetListByAdmin", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spCategoryGetListByAdminPageData(object param)
        {
            object? result = await _connection.QueryAsync("spCategoryGetListByAdminPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spCategoryDeactive(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spCategoryDeactive", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
