using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotLms.Web.Models
{
    public class PageWebModel
    {
        public int Id { get; set; }

        public PageWebModel ParentPage { get; set; }

        public IEnumerable<PageWebModel> ChildPages { get; set; }

        public string Name { get; set; }

        public string UglyName { get; set; }

        public string Url { get; set; }

        public string Author { get; set; }

        public string LastEditedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? LastEditedOn { get; set; }
    }
}
