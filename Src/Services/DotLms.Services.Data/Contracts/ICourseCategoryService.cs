using System.Collections.Generic;
using DotLms.Web.Models;

namespace DotLms.Services.Data.Contracts
{
    public interface ICourseCategoryService
    {
        void CreateNewCategory(CourseCategoryViewModel model);
        CourseCategoryViewModel GetCategoryViewModel(string name);
        IEnumerable<CourseCategoryViewModel> GetAllCategories();
    }
}