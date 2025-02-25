namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<object?> spStoreCreate(object param);
        Task<object?> spStoreUpdate(object param);
        Task<object?> spStoreGetById(object param);
        Task<object> spStoreGetList(object param);
        Task<object?> spStoreChangeStatus(object param);
    }
}
