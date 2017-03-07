using System.Collections.Generic;

namespace DotLms.Data.Models
{
    public class Page
    {      
        public int Id { get; set; }

        public string Name { get; set; }

        public string UglyName { get; set; }

        public string HtmlContent { get; set; }

        public virtual ICollection<Page> ChildPages { get; set; }
    }
}