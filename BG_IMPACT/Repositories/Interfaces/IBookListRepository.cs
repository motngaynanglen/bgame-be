
namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IBookListRepository
    {
        Task<object?> spBookListCreate(object param);
        Task<object?> spBookListEnd(object param);
        Task<object?> spBookListGet(object param);
        Task<object?> spBookListExtend(object param);
        Task<object?> spBookListGetPageData(object param);
        Task<object?> spBookListStart(object param);
        Task<object?> spBookListHistory(object param);
        Task<object?> spBookListHistoryPageData(object param);
        Task<object?> spBookListGetAvailableSlot(object param);
        Task<object?> spBookListGetAvailableProduct(object param);
    }
}
