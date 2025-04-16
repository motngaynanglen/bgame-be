
namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<object?> spOrderCreate(object param);
        Task<object?> spOrderHistory(object param);
        Task<object?> spOrderHistoryPageData(object param);
    }
}
