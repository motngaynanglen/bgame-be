namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<object> spAccountCreateManager(object param);
        Task<object> spAccountCreateStaff(object param);
        Task<object> spAccountCreateCustomer(object param);
        Task<object> spAccountLogin(object param);
    }
}
