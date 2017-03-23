using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class CourseService : ICourseService
    {
        private readonly IDotLmsEfData dotLmsEfData;
        private readonly IEntityFrameworkRepository<Course> courseEfRepository;
        private readonly IMapperProvider mapperProvider;

        public CourseService(IEntityFrameworkRepository<Course> courseEfRepository,
            IDotLmsEfData dotLmsEfData,
            IMapperProvider mapperProvider)
        {
            Guard.WhenArgument(courseEfRepository, nameof(courseEfRepository)).IsNull().Throw();
            Guard.WhenArgument(dotLmsEfData, nameof(dotLmsEfData)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();

            this.courseEfRepository = courseEfRepository;
            this.dotLmsEfData = dotLmsEfData;
            this.mapperProvider = mapperProvider;
        }

        public CourseViewModel CreateCourse(CourseCreationViewModel model, MediaItemViewModel image)
        {
            Guard.WhenArgument(model,nameof(model)).IsNull().Throw();
            Guard.WhenArgument(image,nameof(image)).IsNull().Throw();

            Course course = this.mapperProvider.Instance.Map<Course>(model);
            CourseCategory courseCategory = this.mapperProvider.Instance.Map<CourseCategory>(model);

            course.Category = courseCategory;
            course.UglyName = this.GeneratUglyName(course.Name);
            course.Url = this.GenereateUrl(course.UglyName);
            course.MainImageId = image.Id;
            course.CategoryId = courseCategory.Id;

            this.courseEfRepository.Add(course);
            this.dotLmsEfData.SaveChanges();
            return this.mapperProvider.Instance.Map<CourseViewModel>(course);
        }

        public CourseViewModel GetCourseViewModel(string name)
        {
            Course foundCourse = this.courseEfRepository
                .All
                .Where(x => x.UglyName == name)
                .Include(x => x.Category)
                .Include(x => x.MainImage)
                .FirstOrDefault();

            CourseViewModel mappedCourseViewModel = this.mapperProvider.Instance.Map<CourseViewModel>(foundCourse);
            return mappedCourseViewModel;
        }

        public CourseCreationViewModel GetCourseCreationViewModel(string name)
        {
            Course foundCourse = this.courseEfRepository
                .All
                .Where(x => x.UglyName == name)
                .Include(x => x.Category)
                .Include(x => x.MainImage)
                .FirstOrDefault();

            CourseCreationViewModel mappedCourseCreationViewModel = this.mapperProvider.Instance.Map<CourseCreationViewModel>(foundCourse);
            return mappedCourseCreationViewModel;
        }

        public IEnumerable<CourseViewModel> GetAllCourseViewModels()
        {
            IEnumerable<Course> courses = this.courseEfRepository
                .All
                .Include(x => x.Category)
                .Include(x => x.MainImage)
                .ToList(); ;

            IEnumerable<CourseViewModel> courseViewModels = this.mapperProvider
                .Instance
                .Map<IEnumerable<CourseViewModel>>(courses);

            return courseViewModels;
        }

        public Course UpdateCourse(CourseCreationViewModel model)
        {
            Course courseToUpdate = this.courseEfRepository
                .All
                .Where(x => x.UglyName == model.Name)
                .Include(x => x.Category)
                .Include(x => x.MainImage)
                .FirstOrDefault();

            Course mappedCourse = this.mapperProvider.Instance.Map<Course>(model);
            CourseCategory courseCategory = this.mapperProvider.Instance.Map<CourseCategory>(model);

            courseToUpdate.Name = mappedCourse.Name;
            courseToUpdate.ShortDescription = mappedCourse.ShortDescription;
            courseToUpdate.FullDescription = mappedCourse.FullDescription;
            courseToUpdate.UglyName = this.GeneratUglyName(mappedCourse.Name);
            courseToUpdate.Url = this.GenereateUrl(courseToUpdate.UglyName);
            courseToUpdate.CategoryId = courseCategory.Id;

            this.courseEfRepository.Update(courseToUpdate);
            this.dotLmsEfData.SaveChanges();
            return courseToUpdate;
        }

        public Course UpdateCourse(CourseCreationViewModel model, MediaItemViewModel image)
        {
            Course courseToUpdate = this.courseEfRepository
                .All
                .Where(x => x.UglyName == model.Name)
                .Include(x => x.Category)
                .Include(x => x.MainImage)
                .FirstOrDefault();

            Course mappedCourse = this.mapperProvider.Instance.Map<Course>(model);
            CourseCategory courseCategory = this.mapperProvider.Instance.Map<CourseCategory>(model);

            courseToUpdate.Name = mappedCourse.Name;
            courseToUpdate.ShortDescription = mappedCourse.ShortDescription;
            courseToUpdate.FullDescription = mappedCourse.FullDescription;
            courseToUpdate.MainImageId = image.Id;
            courseToUpdate.UglyName = this.GeneratUglyName(mappedCourse.Name);
            courseToUpdate.Url = this.GenereateUrl(courseToUpdate.UglyName);
            courseToUpdate.CategoryId = courseCategory.Id;

            this.courseEfRepository.Update(courseToUpdate);
            this.dotLmsEfData.SaveChanges();
            return courseToUpdate;
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

            string uglyName = originalName
                .ToLowerInvariant()
                .Replace(' ', '-');

            return uglyName;
        }
    }
}