namespace BG_IMPACT.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetName(this HttpContext context)
        {
            return context.User?.Claims?.SingleOrDefault(p => p.Type.Contains("name"))?.Value ?? string.Empty;
        }
    }
}
