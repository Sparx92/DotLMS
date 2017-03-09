using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DotLms.Data.Models;

namespace DotLms.Web.Models
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UglyName { get; set; }

        [AllowHtml]
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public User Author { get; set; }
        public User LastEditedBy { get; set; }
        public bool IsPublished { get; set; }
        public PageViewModel ParentPage { get; set; }
        public ICollection<PageViewModel> ChildPages { get; set; }
    }
}
