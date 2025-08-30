






namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<object?> spProductCreateTemplate(object param);
        Task<(object? products, int totalCount)> spProductGetList(object param);
        Task<object> spProductCreate(object param);
        Task<object> spProductCreateUnknown(object param);
        Task<object> spProductGetListByStoreId(object param);
        Task<object?> spProductChangeToSales(object param);
        Task<object?> spProductChangeToRent(object param);
        Task<object?> spProductGetByMultipleOption(object param);
        Task<object> spProductGetListByGroupRefId(object param);
        Task<object?> spProductGetListInStore(object param);
        Task<object?> spProductGetListInStorePageData(object param);
        Task<object?> spProductGetById(object param);
        Task<object?> spProductGetListPageData(object param);
        Task<object?> spGetProductsByTemplateAndCondition(object param);
        Task<object?> spGetProductsByTemplateAndConditionPageData(object param);
        Task<object?> spProductGetListByStoreIdPageData(object param);
        Task<object?> spProductUpdate(object param);
        Task<object?> spProductGetByCode(object param);
        Task<object?> spProductGetTemplateByAdmin(object param); 
        Task<object?> spProductAddFromSupplyItem(object param);   
    }
}
