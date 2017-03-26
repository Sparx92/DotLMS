using System.Collections.Generic;
using DotLms.Web.Models;

namespace DotLms.Services.Data.Contracts
{
    public interface IPageCreationService
    {
        IEnumerable<PageViewModel> GetAllPages();

        /// <summary>
        /// Creates a front page
        /// </summary>
        /// <param name="model">The Model sent back from the form</param>
        /// <param name="username">The user invoking the method</param>
        void CreatePage(PageViewModel model, string username);

        PageViewModel UpdatePage(PageViewModel model);
    }
}