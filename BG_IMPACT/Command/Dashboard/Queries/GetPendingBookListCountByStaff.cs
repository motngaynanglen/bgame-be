using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Dashboard.Queries
{
    public class GetPendingBookListCountByStaff : IRequest<ResponseObject>
    {
        public class GetPendingBookListCountByStaffHandler : IRequestHandler<GetPendingBookListCountByStaff, ResponseObject>
        {
            private readonly IDashboardRepository _dashboardRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetPendingBookListCountByStaffHandler(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
            {
                _dashboardRepository = dashboardRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetPendingBookListCountByStaff request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;

                object param = new
                {
                    UserID
                };

                var result = await _dashboardRepository.spGetPendingBookListCountToday(param);
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