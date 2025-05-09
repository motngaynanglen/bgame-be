﻿using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace BG_IMPACT.Command.Store.Queries
{
    public class GetStoreIDQuery : IRequest<ResponseObject>
    {
        public class GetStoreIDQueryHandler : IRequestHandler<GetStoreIDQuery, ResponseObject>
        {
            public readonly IHttpContextAccessor _httpContextAccessor;
            public readonly IStoreRepository _storeRepository;

            public GetStoreIDQueryHandler(IHttpContextAccessor httpContextAccessor, IStoreRepository storeRepository)
            {
                _httpContextAccessor = httpContextAccessor;
                _storeRepository = storeRepository;
            }
            public async Task<ResponseObject> Handle(GetStoreIDQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName();
                string? Role = context?.GetRole();

                object param = new
                {
                    UserID,
                    Role
                };

                var result = await _storeRepository.spStoreGetByUserID(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Guid.TryParse(dict["StoreID"].ToString(), out Guid StoreID))
                {
                    response.StatusCode = "200";
                    response.Data = StoreID;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy người dùng.";
                }

                return response;
            }
        }
    }
}
