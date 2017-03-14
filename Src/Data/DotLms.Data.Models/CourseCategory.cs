using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotLms.Data.Models
{
    public class CourseCategory
    {
        public int Id { get; set; } 

        [Required]
        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Name { get; set; }
    }
}