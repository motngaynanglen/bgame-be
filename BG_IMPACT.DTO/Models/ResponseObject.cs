namespace BG_IMPACT.Models
{
    public class ResponseObject
    {
        public string StatusCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
        public PagingModel? Paging { get; set; }
    }

    public class PagingModel
    {
        public long PageNum { get; set; }
        public long PageSize { get; set; }
        public long PageCount { get; set; }
    }
}
