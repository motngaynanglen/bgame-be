using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Dashboard.Queries
{
    public class GetBookListRevenueByManagerQuery : IRequest<ResponseObject>
    {
        public class GetBookListRevenueByManagerQueryHandler : IRequestHandler<GetBookListRevenueByManagerQuery, ResponseObject>
        {
            private readonly IDashboardRepository _dashboardRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetBookListRevenueByManagerQueryHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
            {
                _dashboardRepository = dashboardRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetBookListRevenueByManagerQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? ManagerId = context?.GetName() ?? null;

                object param = new
                {
                    ManagerId
                };

                var result = await _dashboardRepository.spGetBookListRevenueByManager(param);
                var dict = result as IDictionary<string, object>;


                if (dict != null && dict.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thông tin.";
                }

                return response;
            }
        }
    }
}