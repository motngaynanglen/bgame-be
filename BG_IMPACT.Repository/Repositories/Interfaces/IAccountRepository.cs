﻿namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<object?> spAccountCreateManager(object param);
        Task<object?> spAccountCreateStaff(object param);
        Task<object?> spAccountCreateCustomer(object param);
        Task<object?> spLogin(object param);
        Task<object?> spAccountAddRefreshToken(object param);
        Task<object?> spAccountListGetByAdmin();
        Task<object?> spAccountListGetByManager(object param);
        Task<object?> spAccountListGetByManagerPageData(object param);
        Task<object?> spUpdateStaffProfile(object param);
        Task<object?> spAccountReverseStaffStatus(object param);
        Task<object?> spAccountReverseStatusForAdmin(object param);
        Task<object?> spGetCustomerListByPhoneAndEmail(object param);
        Task<object?> spAccountGetProfile(object param);
    }
}
