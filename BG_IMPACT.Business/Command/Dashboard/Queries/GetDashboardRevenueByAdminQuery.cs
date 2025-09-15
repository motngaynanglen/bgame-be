using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetDashboardRevenueByAdminQuery : IRequest<ResponseObject>
    {
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }

        public class GetDashboardRevenueByAdminQueryHandler : IRequestHandler<GetDashboardRevenueByAdminQuery, ResponseObject>
        {
            private readonly IDashboardRepository _dashboardRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetDashboardRevenueByAdminQueryHandler(IHttpContextAccessor httpContextAccessor, IDashboardRepository dashboardRepository)
            {
                _httpContextAccessor = httpContextAccessor;
                _dashboardRepository = dashboardRepository;
            }

            public async Task<ResponseObject> Handle(GetDashboardRevenueByAdminQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                object param = new
                {
                    request.From,
                    request.To
                };

                var result = await _dashboardRepository.spDashboardRevenueByAdmin(param);
                var dict = result as IDictionary<string, object>;

                response.StatusCode = "200";
                response.Data = dict;
                response.Message = string.Empty;

                return response;
            }
        }
    }
}
