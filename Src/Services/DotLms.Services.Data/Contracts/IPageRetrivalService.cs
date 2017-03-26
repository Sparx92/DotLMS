using DotLms.Web.Models;
using DotLms.Web.Models.Backoffice;

namespace DotLms.Services.Data.Contracts
{
    public interface IPageRetrivalService
    {
        PageViewModel GetPage(string pageName);
        PageViewModel GetPage(int pageId);
        BackOfficeIndexViewModel GetAllPages();
    }
}