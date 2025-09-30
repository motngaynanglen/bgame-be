namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IProductGroupRepository
    {
        Task<object?> spProductGroupCreate(object param);
        Task<object> spProductGroupGetList();
        Task<object?> spProductGroupUpdate(object param);
    }
}
