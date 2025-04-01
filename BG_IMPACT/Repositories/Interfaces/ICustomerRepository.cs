namespace BG_IMPACT.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<object?> spCustomerGetList(object param);
        Task<object?> spCustomerUpdateProfile(object param);

    }
}
