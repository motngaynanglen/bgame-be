using BG_IMPACT.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BG_IMPACT.Repositories.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly SqlConnection _connection;

        public DashboardRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }
        public async Task<object?> spGetTodayOrderRevenueByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetTodayOrderRevenueByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetTodayBookListRevenueByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetTodayBookListRevenueByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetPendingOrdersCountByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetPendingOrdersCountByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetTodayActiveOrdersByStaff(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetTodayActiveOrdersByStaff", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetPendingBookListCountToday(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetPendingBookListCountToday", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetOrderRevenueByManager(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetOrderRevenueByManager", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetBookListRevenueByManager(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetBooklistRevenueByManager", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetOrderCountByManager(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetOrderCountByManager", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetBookListCountByManager(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spGetBookListCountByManager", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetRevenuePerDayByMonth(object param)
        {
            object? result = await _connection.QueryAsync("spGetRevenuePerDayByMonth", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<object?> spGetRevenuePerMonth(object param)
        {
            object? result = await _connection.QueryAsync("spGetRevenuePerMonth", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        

    }
}

