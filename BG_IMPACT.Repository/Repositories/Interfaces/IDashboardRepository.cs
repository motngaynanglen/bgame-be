namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<object?> spGetTodayOrderRevenueByStaff(object param);
        Task<object?> spGetTodayBookListRevenueByStaff(object param);
        Task<object?> spGetPendingOrdersCountByStaff(object param);
        Task<object?> spGetTodayActiveOrdersByStaff(object param);
        Task<object?> spGetPendingBookListCountToday(object param);
        Task<object?> spGetOrderRevenueByManager(object param);
        Task<object?> spGetBookListRevenueByManager(object param);
        Task<object?> spGetOrderCountByManager(object param);
        Task<object?> spGetBookListCountByManager(object param);
        Task<object?> spGetRevenuePerDayByMonth(object param);
        Task<object?> spGetRevenuePerMonth(object param);
        Task<object?> spDashboardAdminRevenue();
        Task<object?> spDashboardStatisticsByAdmin();
        Task<object?> spDashboardRevenueByAdmin(object param);

    }
}
