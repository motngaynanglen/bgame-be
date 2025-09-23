namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IStoreTableRepository
    {
        Task<object?> spStoreTableCreate(object param);
        Task<object?> spStoreTableUpdate(object param);
        Task<object?> spStoreTableDisableById(object param);
        Task<object?> spStoreTableGetListByStoreID(object param);
        Task<object?> spStoreTableGetBookListByDate(object param);

    }
}
