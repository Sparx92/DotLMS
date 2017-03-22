using System.Web;
using DotLms.Web.Models;

namespace DotLms.Services.Data.Contracts
{
    public interface IFileService
    {
        MediaItemViewModel SaveFile(HttpPostedFileBase fileBase);
    }
}