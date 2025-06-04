namespace BG_IMPACT.DTO.Models
{
    public class Filter
    {
        public long? MinPrice { get; set; }
        public long? MaxPrice { get; set; }
        public long? MaxNumberOfPlayer { get; set; }
        public long? MinNumberOfPlayers { get; set; }
        public long? Age { get; set; }
        public long? Duration { get; set; }
        public List<string>? Categories { get; set; } = [];
        public bool? InStock { get; set; }
    }
}
