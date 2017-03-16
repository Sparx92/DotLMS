using System.Collections.Generic;

namespace DotLms.Web.Models
{
    public class CourseListViewModel
    {
        public IEnumerable<CourseViewModel> CourseViewModels { get; set; }

        public IEnumerable<CourseCategoryViewModel> CourseCategoryViewModels { get; set; }
    }
}