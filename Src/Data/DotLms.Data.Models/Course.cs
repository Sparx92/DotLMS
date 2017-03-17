using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotLms.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(30)]
        public string UglyName { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(30)]
        public string Url { get; set; }

        public int CategoryId { get; set; }

        public virtual CourseCategory Category { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int MainImageId { get; set; }

        public virtual MediaItem MainImage { get; set; }

        public virtual ICollection<Page> ChildPages { get; set; }
    }
}