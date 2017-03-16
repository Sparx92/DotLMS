using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class CourseService
    {
        private IDotLmsEfData dotLmsEfData;
        private CourseEfRepository courseEfRepository;
        private IEntityFrameworkRepository<CourseCategory> courseCategoryEfRepository;
        private IMapperProvider mapperProvider;
        private IProjectableRepository<Course> projectableCourseRepository;

        public CourseService(CourseEfRepository courseEfRepository,
            IEntityFrameworkRepository<CourseCategory> courseCategoryEfRepository,
            IDotLmsEfData dotLmsEfData,
            IMapperProvider mapperProvider,
            IProjectableRepository<Course> projectableCourseRepository)
        {
            Guard.WhenArgument(courseEfRepository, nameof(courseEfRepository)).IsNull().Throw();
            Guard.WhenArgument(courseCategoryEfRepository, nameof(courseCategoryEfRepository)).IsNull().Throw();
            Guard.WhenArgument(dotLmsEfData, nameof(dotLmsEfData)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();
            Guard.WhenArgument(projectableCourseRepository, nameof(projectableCourseRepository)).IsNull().Throw();

            this.courseEfRepository = courseEfRepository;
            this.courseCategoryEfRepository = courseCategoryEfRepository;
            this.dotLmsEfData = dotLmsEfData;
            this.mapperProvider = mapperProvider;
            this.projectableCourseRepository = projectableCourseRepository;
        }

        public void CreateCourse(CourseCreationViewModel model, MediaItem image)
        {
            Course course = this.mapperProvider.Instance.Map<Course>(model);
            CourseCategory courseCategory = this.mapperProvider.Instance.Map<CourseCategory>(model);

            course.Category = courseCategory;
            course.MainImage = image;
            course.UglyName = this.GeneratUglyName(course.Name);
            course.Url = this.GenereateUrl(course.UglyName);

            this.courseEfRepository.Add(course);
            this.dotLmsEfData.Commit();
        }

        public CourseViewModel GetCourseViewModel(string name)
        {
            Course foundCourse = this.courseEfRepository.GetCourse(name);

            CourseViewModel mappedCourseViewModel = this.mapperProvider.Instance.Map<CourseViewModel>(foundCourse);
            return mappedCourseViewModel;
        }

        public IEnumerable<CourseViewModel> GetAllCourseViewModels()
        {
            IEnumerable<Course> courses = this.courseEfRepository.GetAllCourses();

            var m = this.mapperProvider.Instance.Map<IEnumerable<CourseViewModel>>(courses);
            return m;
        }


        private string GenereateUrl(string uglyName)
        {
            Guard.WhenArgument(uglyName, nameof(uglyName)).IsNullOrEmpty().Throw();
            
            string url = $"/{uglyName}";
            return url;
        }

        private string GeneratUglyName(string originalName)
        {
            if (originalName.IndexOf(" ", StringComparison.Ordinal) < 0)
            {
                return originalName.ToLowerInvariant();
            }

            var uglyName = originalName
                .ToLowerInvariant()
                .Replace(' ', '-');

            return uglyName;
        }
    }
}