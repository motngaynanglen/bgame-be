using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetDashboardByAminQuery : IRequest<ResponseObject>
    {
        public DateTimeOffset CurrentDate { get; set; }
    }

    public class GetDashboardByAminQueryHandler : IRequestHandler<GetDashboardByAminQuery, ResponseObject>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDashboardRepository _dashboardRepository;

        public GetDashboardByAminQueryHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject> Handle(GetDashboardByAminQuery request, CancellationToken cancellationToken)
        {
            ResponseObject response = new();

            object param = new
            {
                request.CurrentDate
            };

            // Lấy dữ liệu từ repository
            var (summary, topStores) = await _dashboardRepository.spDashboardAdmin(param);

            response.StatusCode = "200";
            response.Data = new
            {
                summary,
                topStores
            };
            response.Message = string.Empty;

            return response;
        }
    }
}