

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
    }
}
