namespace BG_IMPACT.Repositories.Interfaces
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
