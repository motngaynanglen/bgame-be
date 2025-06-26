using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetActiveBookListsCountByStaffQuery : IRequest<ResponseObject>
    {
        public class GetActiveBookListsCountByStaffQueryHandler : IRequestHandler<GetActiveBookListsCountByStaffQuery, ResponseObject>
        {
            private readonly IDashboardRepository _dashboardRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetActiveBookListsCountByStaffQueryHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
            {
                _dashboardRepository = dashboardRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetActiveBookListsCountByStaffQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;

                object param = new
                {
                    UserID
                };

                var result = await _dashboardRepository.spDashboardCountTodayActiveBookListByStaff(param);
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