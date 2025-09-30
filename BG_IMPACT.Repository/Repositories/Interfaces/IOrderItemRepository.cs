
namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<object?> spOrderItemUpdateProduct(object param);
        Task<object?> spOrderItemGetItemsByOrderIds(object param);


    }
}
