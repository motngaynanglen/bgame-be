using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Models
{
    public class Paging
    {
        [Required]
        [Range(1, int.MaxValue)]
        public long PageNum { get; set; } = 0;
        [Required]
        [Range(1, 20)]
        public long PageSize { get; set; } = 0;
    }
}
