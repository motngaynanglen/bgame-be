using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetDashboardByManagerQuery : IRequest<ResponseObject>
    {
        public string Period { get; set; }
    }

    public class GetDashboardManagerQueryHandler : IRequestHandler<GetDashboardByManagerQuery, ResponseObject>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDashboardRepository _dashboardRepository;

        public GetDashboardManagerQueryHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject> Handle(GetDashboardByManagerQuery request, CancellationToken cancellationToken)
        {
            ResponseObject response = new();
            var context = _httpContextAccessor.HttpContext;
            string? UserID = context?.GetName() ?? null;

            object param = new
            {
                request.Period,
                UserID
            };

            // Gọi repo lấy đủ 3 dataset
            var (revenue, summary, topProduct) = await _dashboardRepository.spDashboardManager(param);

            response.StatusCode = "200";
            response.Data = new
            {
                revenue,   // chart dữ liệu theo ngày/tháng
                summary,   // tổng quan
                topProduct  // top 3 cửa hàng
            };
            response.Message = string.Empty;

            return response;
        }

    }
}