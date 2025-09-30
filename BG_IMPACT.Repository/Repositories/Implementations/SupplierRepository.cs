using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly SqlConnection _connection;

        public SupplierRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object> spSupplierCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spSupplierCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spSupplierUpdate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spSupplierUpdate", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spSupplierGetList()
        {
            object? result = await _connection.QueryAsync("spSupplierGetList", commandType: CommandType.StoredProcedure);
            return result;
        }


    }
}
