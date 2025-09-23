using BG_IMPACT.Repositories.Interfaces;
using Dapper;
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
        public async Task<object?> spAccountListGetByAdmin()
        {
            object? result = await _connection.QueryAsync("spAccountListGetByAdmin", commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spAccountListGetByManager(object param)
        {
            object? result = await _connection.QueryAsync("spAccountListGetByManager", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spAccountListGetByManagerPageData(object param)
        {
            object? result = await _connection.QueryAsync("spAccountListGetByManagerPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spUpdateStaffProfile(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spUpdateStaffProfile", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spAccountReverseStaffStatus(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spAccountReverseStaffStatus", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spAccountReverseStatusForAdmin(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spAccountReverseStatusForAdmin", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spGetCustomerListByPhoneAndEmail(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetCustomerListByPhoneAndEmail", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spAccountGetProfile(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spAccountGetProfile", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spCustomerGetById(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spCustomerGetById", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spCustomerGetByCode(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spCustomerGetByCode", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
