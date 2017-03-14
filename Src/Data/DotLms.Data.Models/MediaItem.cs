using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotLms.Data.Models
{
    public class MediaItem
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(80)]
        public string Name { get; set; }

        [StringLength(80)]
        public string FullName { get; set; }

        [StringLength(10)]
        public string Extension { get; set; }

        [Required]
        [StringLength(80)]
        public string OriginalName { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string Url { get; set; }
    }
}