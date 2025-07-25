using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repositories.Implementations
{
    public class BookTableRepository : IBookTableRepository
    {
        private readonly SqlConnection _connection;

        public BookTableRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object?> spBookTableCreateByCustomer(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spBookTableCreateByCustomer", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
