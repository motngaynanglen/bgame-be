﻿namespace BG_IMPACT.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<object> spSupplierCreate(object param);
        Task<object> spSupplierUpdate(object param);
        Task<object> spSupplierGetList();
    }
}

