using BG_IMPACT.Repository.Repositories.Interfaces;
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

        public async Task<object?> spCustomerAddPointByAdmin(object param)
        {
            var result = await _connection.QueryFirstOrDefaultAsync(
                            "spCustomerAddPointByAdmin",
                            param,
                            commandType: CommandType.StoredProcedure
                        );
            return result;
        }

        public async Task<object?> spCustomerGetList(object param)
        {
            var result = await _connection.QueryAsync<object>(
                "spCustomerGetList",
                param,
                commandType: CommandType.StoredProcedure
            );
            return result;
        }
        public async Task<object?> spCustomerUpdateProfile(object param)
        {
            var result = await _connection.QueryFirstOrDefaultAsync(
                "spCustomerUpdateProfile",
                param,
                commandType: CommandType.StoredProcedure
            );
            return result;
        }

        public async Task<object?> spCustomerUpdateAddress(object param)
        {
            var result = await _connection.QueryFirstOrDefaultAsync(
                "spCustomerUpdateAddress",
                param,
                commandType: CommandType.StoredProcedure
            );
            return result;
        }

    }
}
