using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotLms.Data.Models
{
    public class Page
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string UglyName { get; set; }

        [Required]
        [MinLength(1)]
        public string HtmlContent { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Url { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? LastEditedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public User Author { get; set; }

        public User LastEditedBy { get; set; }

        public bool IsPublished { get; set; }

        public int ParentCourseId { get; set; }

        public virtual Course ParentCourse { get; set; }
    }
}