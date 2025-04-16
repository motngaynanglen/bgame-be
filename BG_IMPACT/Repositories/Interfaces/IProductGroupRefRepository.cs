namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IProductGroupRefRepository
    {
        Task<object?> spProductGroupRefCreate(object param);
        Task<object> spProductGroupRefGetList(object param); 
        Task<object> spProductGroupRefUpdate(object param);
        

    }
}
