namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<object?> spGetTodayOrderRevenueByStaff(object param);
        Task<object?> spGetTodayBookListRevenueByStaff(object param);
        Task<object?> spGetPendingOrdersCountByStaff(object param);
        Task<object?> spGetTodayActiveOrdersByStaff(object param);
        Task<object?> spGetPendingBookListCountToday(object param);
    }
}
