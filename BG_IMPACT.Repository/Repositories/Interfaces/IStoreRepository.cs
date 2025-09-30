namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<object?> spStoreCreate(object param);
        Task<object?> spStoreUpdate(object param);
        Task<object?> spStoreGetByIdByAdmin(object param);
        Task<object> spStoreGetList(object param);
        Task<object> spStoreGetRentals(object param);
        Task<object?> spStoreChangeStatus(object param);
        Task<object> spStoreGetListByGroupRefId(object param);
        Task<object?> spStoreGetByUserID(object param);
        Task<object?> spStoreGetListAndProductCountById(object param);
    }
}
