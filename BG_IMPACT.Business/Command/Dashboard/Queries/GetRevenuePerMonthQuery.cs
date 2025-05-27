namespace BG_IMPACT.Business.Command.Dashboard.Queries
{
    public class GetRevenuePerMonthQuery : IRequest<ResponseObject>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class GetRevenuePerMonthQueryHandler : IRequestHandler<GetRevenuePerMonthQuery, ResponseObject>
        {
            private readonly IDashboardRepository _dashboardRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetRevenuePerMonthQueryHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
            {
                _dashboardRepository = dashboardRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetRevenuePerMonthQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? ManagerId = context?.GetName() ?? null;

                object param = new
                {
                    ManagerId,
                    request.StartDate,
                    request.EndDate,
                };

                var result = await _dashboardRepository.spGetRevenuePerMonth(param);
                //var dict = result as IDictionary<string, object>;
                var dict = ((IEnumerable<dynamic>)result).ToList();

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