﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using DotLms.Data.Models;

namespace DotLms.Web.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UglyName { get; set; }

        public string Url { get; set; }

        public CourseCategory Category { get; set; }

        [AllowHtml]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [DisplayName("Full Description")]
        public string FullDescription { get; set; }

        public MediaItem MainImage { get; set; }

        public virtual ICollection<PageViewModel> ChildPages { get; set; }
    }
}