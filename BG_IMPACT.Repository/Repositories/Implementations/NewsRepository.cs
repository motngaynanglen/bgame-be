using BG_IMPACT.Repositories.Interfaces;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using BG_IMPACT.Repositories.Interfaces;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace BG_IMPACT.Repository.Repositories.Implementations
{
    public class NewsRepository : INewsRepository
    {
        private readonly SqlConnection _connection;

        public NewsRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object> spNewsCreate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spNewsCreate", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spNewsUpdate(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spNewsUpdate", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spNewsGetList(object param)
        {
            object? result = await _connection.QueryAsync("spNewsGetList", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object> spNewsGetListPageData(object param)
        {
            object? result = await _connection.QueryAsync("spNewsGetListPageData", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object> spNewsDeactive(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spNewsDeactive", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        



    }
}
