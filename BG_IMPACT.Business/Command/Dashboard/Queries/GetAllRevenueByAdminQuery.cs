using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetAllRevenueByAdminQuery : IRequest<ResponseObject>
    {
        public class GetAllRevenueByAdminQueryHandler : IRequestHandler<GetAllRevenueByAdminQuery, ResponseObject>
        {
            private readonly IDashboardRepository _dashboardRepository;

            public GetAllRevenueByAdminQueryHandler(IDashboardRepository dashboardRepository)
            {
                _dashboardRepository = dashboardRepository;
            }
            public async Task<ResponseObject> Handle(GetAllRevenueByAdminQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var result = await _dashboardRepository.spDashboardAdminRevenue();
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