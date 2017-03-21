using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace DotLms.Web.Models
{
    public class CourseCreationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UglyName { get; set; }

        public int CategoryId { get; set; }

        public CourseCategoryViewModel Category { get; set; }

        public IEnumerable<CourseCategoryViewModel> Categories { get; set; }

        [AllowHtml]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [DisplayName("Full Description")]
        public string FullDescription { get; set; }

        public HttpPostedFileBase File { get; set; }

        public IEnumerable<PageViewModel> ChildPages { get; set; }
    }
}