namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IConsignmentOrderRepository
    {
        Task<object?> spConsignmentOrderGetById(object param);
        Task<object?> spConsignmentOrderGetList(object param);
        Task<object?> spConsignmentOrderCreateByStaff(object param);
        Task<object?> spConsignmentOrderCancelByStaff(object param);
        Task<object?> spConsignmentOrderUpdateByStaff(object param);

    }
}
