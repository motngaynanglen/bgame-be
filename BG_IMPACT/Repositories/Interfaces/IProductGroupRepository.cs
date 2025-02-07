namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IProductGroupRepository
    {
        Task<object> spProductGroupCreate(object param);
        Task<object> spProductGroupGetList();
    }
}
