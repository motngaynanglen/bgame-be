using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetDashboardStatisticsByAdminQuery : IRequest<ResponseObject>
    {
        public class GetDashboardStatisticsByAdminQueryHandler : IRequestHandler<GetDashboardStatisticsByAdminQuery, ResponseObject>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IDashboardRepository _dashboardRepository;

            public GetDashboardStatisticsByAdminQueryHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
            {
                _dashboardRepository = dashboardRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(GetDashboardStatisticsByAdminQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                var result = await _dashboardRepository.spDashboardStatisticsByAdmin();
                var dict = result as IDictionary<string, object>;

                response.StatusCode = "200";
                response.Data = dict;
                response.Message = string.Empty;

                return response;
            }
        }
    }
}
