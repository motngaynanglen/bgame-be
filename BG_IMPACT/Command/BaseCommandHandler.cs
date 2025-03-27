namespace BG_IMPACT.Command
{
    public class BaseCommandHandler
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public BaseCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
