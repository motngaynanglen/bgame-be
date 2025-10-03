using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetDashboardByAminQuery : IRequest<ResponseObject>
    {
        public string Period { get; set; }
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
                request.Period
            };

            // Gọi repo lấy đủ 3 dataset
            var (revenue, summary, topStores) = await _dashboardRepository.spDashboardAdmin(param);

            response.StatusCode = "200";
            response.Data = new
            {
                revenue,   // chart dữ liệu theo ngày/tháng
                summary,   // tổng quan
                topStores  // top 3 cửa hàng
            };
            response.Message = string.Empty;

            return response;
        }

    }
}