using System.Collections.Generic;
using DotLms.Data.Models;
using DotLms.Web.Models;

namespace DotLms.Services.Data.Contracts
{
    public interface ICourseService
    {
        CourseViewModel CreateCourse(CourseCreationViewModel model, MediaItemViewModel image);
        CourseViewModel GetCourseViewModel(string name);
        CourseCreationViewModel GetCourseCreationViewModel(string name);
        IEnumerable<CourseViewModel> GetAllCourseViewModels();
        Course UpdateCourse(CourseCreationViewModel model);
        Course UpdateCourse(CourseCreationViewModel model, MediaItemViewModel image);
    }
}