
namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<object?> spOrderCreate(object param);
        Task<object?> spOrderCreateByCustomer(object param);
        Task<object?> spOrderCreateByStaff(object param);
        Task<object?> spOrderHistory(object param);
        Task<object?> spOrderHistoryPageData(object param);
        Task<object?> spOrderClaimRequest(object param);
        Task<object?> spOrderUpdateDeliveryInfo(object param);
        Task<object?> spOrderUpdateStatusToPaid(object param);
        Task<object?> spOrderUpdateStatusToSending(object param);
        Task<object?> spOrderUpdateStatusToSent(object param);
        Task<object?> spOrderUpdateStatusToPrepare(object param);
        Task<object?> spOrderGetById(object param);
        Task<object?> spOrderGetUnclaim(object param);
        Task<object?> spOrderGetUnclaimPageData(object param);
        Task<object?> spOrderCancel(object param);

    }
}
