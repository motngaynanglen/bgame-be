namespace BG_IMPACT.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<object?> spTransactionCreateOffline(object param);
    }
}
