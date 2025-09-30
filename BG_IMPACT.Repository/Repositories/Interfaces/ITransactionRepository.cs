
namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<object?> spCheckOnlinePayment(object param);
        Task<object?> spTransactionCreateOffline(object param);
        Task<object?> spTransactionGetItemByRefId(object param);
        Task<object?> spTransactionUpdatePaymentRef(object param);
        Task<object?> spTransactionGetByRefId(object param);
        Task<object?> MarkOrderAsPaid (object param) => spCheckOnlinePayment(param);
    }
}
