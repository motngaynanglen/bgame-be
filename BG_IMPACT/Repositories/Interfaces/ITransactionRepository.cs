
namespace BG_IMPACT.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<object?> spCheckOnlinePayment(object param);
        Task<object?> spTransactionCreateOffline(object param);
        Task<object?> spTransactionGetItemByRefId(object param);
    }
}
