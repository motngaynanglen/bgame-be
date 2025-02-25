namespace BG_IMPACT.Models
{
    public class ResponseObject
    {
        public string StatusCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
