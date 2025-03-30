using BG_IMPACT.Repositories.Interfaces;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace BG_IMPACT.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _connection;

        public CustomerRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object?> spCustomerGetList()
        {
            var result = await _connection.QueryAsync<object>(
                "spCustomerGetList",
                commandType: CommandType.StoredProcedure
            );
            return result;
        }
        public async Task<object?> spCustomerUpdateProfile(object param)
        {
            var result = await _connection.ExecuteAsync(
                "spCustomerUpdateProfile",
                param,
                commandType: CommandType.StoredProcedure
            );
            return result;
        }

    }
}
