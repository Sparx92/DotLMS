using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DotLms.Web.Models
{
    public class MediaItemViewModel
    {
        public HttpPostedFileBase File { get; set; }
    }
}