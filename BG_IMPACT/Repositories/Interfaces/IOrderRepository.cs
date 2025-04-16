
namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<object?> spOrderCreate(object param);
        Task<object?> spOrderHistory(object param);
        Task<object?> spOrderHistoryPageData(object param);
        Task<object?> spOrderUpdateStatusToPaid(object param);
        Task<object?> spOrderUpdateStatusToSending(object param);
        Task<object?> spOrderUpdateStatusToSent(object param);
    }
}
