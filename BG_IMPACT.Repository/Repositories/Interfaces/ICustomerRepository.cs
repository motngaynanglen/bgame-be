namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<object?> spCustomerGetList(object param);
        Task<object?> spCustomerUpdateProfile(object param);
        Task<object?> spCustomerAddPointByAdmin(object param);
        Task<object?> spCustomerUpdateAddress(object param);
    }
}
