using BG_IMPACT.Repository.Repositories.Interfaces;
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
        public async Task<object?> spDashboardAdminRevenue()
        {
            object? result = await _connection.QueryAsync("spDashboardAdminRevenue", commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spDashboardStatisticsByAdmin()
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spDashboardStatisticsByAdmin", commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<object?> spDashboardRevenueByAdmin(object param)
        {
            object? result = await _connection.QueryFirstOrDefaultAsync("spDashboardRevenueByAdmin", param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<(IEnumerable<object> revenue, object summary, IEnumerable<object> topStores)> spDashboardAdmin(object param)
        {
            using var multi = await _connection.QueryMultipleAsync(
                "spDashboardAdmin",
                param,
                commandType: CommandType.StoredProcedure
            );

            // 1. Lấy revenue (nhiều dòng)
            var revenue = (await multi.ReadAsync<object>()).ToList();

            // 2. Lấy summary (1 dòng)
            var summary = await multi.ReadFirstOrDefaultAsync<object>();

            // 3. Lấy topStores (nhiều dòng)
            var topStores = (await multi.ReadAsync<object>()).ToList();

            return (revenue, summary, topStores);
        }

    }
}

