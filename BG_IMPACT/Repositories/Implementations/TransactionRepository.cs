using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SqlConnection _connection;

        public TransactionRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }

        public async Task<object?> spCheckOnlinePayment(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spCheckOnlinePayment", param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spTransactionCreateOffline(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spTransactionCreateOffline", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spTransactionGetItemByRefId(object param)
        {
            object? result = await _connection.QueryAsync("spTransactionGetItemByRefId", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
