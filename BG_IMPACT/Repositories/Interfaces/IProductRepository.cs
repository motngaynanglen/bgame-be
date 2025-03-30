

namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<object?> spProductCreateTemplate(object param);
        Task<object> spProductGetList(object param);
        Task<object> spProductCreate(object param);
        Task<object> spProductCreateUnknown(object param);
        Task<object> spProductGetListByStoreId(object param);
        Task<object?> spProductChangeToSales(object param);
        Task<object?> spProductChangeToRent(object param);
        Task<object?> spProductGetByMultipleOption(object param);
        Task<object?> spProductGetListInStore(object param);
        Task<object?> spProductGetListInStorePageData(object param);

    }
}
