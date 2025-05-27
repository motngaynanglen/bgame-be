namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetActiveOrdersCountByStaffQuery : IRequest<ResponseObject>
    {
        public class GetActiveOrdersCountByStaffQueryHandler : IRequestHandler<GetActiveOrdersCountByStaffQuery, ResponseObject>
        {
            private readonly IDashboardRepository _dashboardRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetActiveOrdersCountByStaffQueryHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
            {
                _dashboardRepository = dashboardRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetActiveOrdersCountByStaffQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;

                object param = new
                {
                    UserID
                };

                var result = await _dashboardRepository.spGetTodayActiveOrdersByStaff(param);
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