using System.Web;

namespace DotLms.Web.Models
{
    public class MediaItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Extension { get; set; }

        public string OriginalName { get; set; }

        public string FileType { get; set; }

        public string Url { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}