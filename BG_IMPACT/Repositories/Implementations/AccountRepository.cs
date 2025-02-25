using Dapper;
using BG_IMPACT.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SqlConnection _connection;

        public AccountRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object?> spAccountCreateCustomer(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spAccountCreateCustomer", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spLogin(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spLogin", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spAccountCreateManager(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spAccountCreateManager", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spAccountCreateStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spAccountCreateStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spAccountAddRefreshToken(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spAccountAddRefreshToken", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
