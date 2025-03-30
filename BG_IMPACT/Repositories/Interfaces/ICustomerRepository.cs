namespace BG_IMPACT.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<object?> spCustomerGetList();
        Task<object?> spCustomerUpdateProfile(object param);

    }
}
