namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<object?> spOrderCreate(object param);
    }
}
