﻿namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<object> spProductCreateTemplate(object param);
        Task<object> spProductGetList(object param);
    }
}
