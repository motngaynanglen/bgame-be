﻿using Dapper;
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

        public async Task<object> spAccountCreateCustomer(object param)
        {
            object result = await _connection.ExecuteAsync("spAccountCreateCustomer", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spAccountLogin(object param)
        {
            object result = await _connection.ExecuteAsync("spLogin", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spAccountCreateManager(object param)
        {
            object result = await _connection.ExecuteAsync("spAccountManagerCustomer", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spAccountCreateStaff(object param)
        {
            object result = await _connection.ExecuteAsync("spAccountStaffCustomer", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
