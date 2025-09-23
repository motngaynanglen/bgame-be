
using System.Data;
using System.Data.Common;

namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IBookListRepository
    {
        Task<object?> spBookListCreateByCustomer(object param);
        Task<object?> spBookListCreateByStaff(object param);
        Task<object?> spBookListEnd(object param);
        Task<(object? bookLists, int totalCount)> spBookListGet(object param);
        Task<object?> spBookListExtend(object param);
        Task<(object? bookLists, int totalCount)> spBookListGetPaged(object param);
        Task<object?> spBookListStart(object param);
        Task<object?> spBookListHistory(object param);
        Task<object?> spBookListHistoryPageData(object param);
        Task<object?> spBookListGetAvailableSlot(object param);
        Task<object?> spBookListGetAvailableProduct(object param);
        Task<object?> spBookListGetById(object param);
        Task<object?> spBookListGetPageData(object param);
        Task<object?> spBookListCancel(object param);
        Task<object?> spBookListGetStoreTimeTableByDate(object param);
        Task<object?> spBookListChangePublicFlag(object param);
    }
}
