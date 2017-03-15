using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class CourseService
    {
        private IDotLmsEfData dotLmsEfData;
        private IEntityFrameworkRepository<Course> courseEfRepository;
        private IEntityFrameworkRepository<CourseCategory> courseCategoryEfRepository;
        private IMapperProvider mapperProvider;

        public CourseService(IEntityFrameworkRepository<Course> courseEfRepository,
            IEntityFrameworkRepository<CourseCategory> courseCategoryEfRepository,
            IDotLmsEfData dotLmsEfData,
            IMapperProvider mapperProvider)
        {
            Guard.WhenArgument(courseEfRepository, nameof(courseEfRepository)).IsNull().Throw();
            Guard.WhenArgument(courseCategoryEfRepository, nameof(courseCategoryEfRepository)).IsNull().Throw();
            Guard.WhenArgument(dotLmsEfData, nameof(dotLmsEfData)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();

            this.courseEfRepository = courseEfRepository;
            this.courseCategoryEfRepository = courseCategoryEfRepository;
            this.dotLmsEfData = dotLmsEfData;
            this.mapperProvider = mapperProvider;
        }

        public void CreateCourse(CourseCreationViewModel model, MediaItem image)
        {
            Course course = this.mapperProvider.Instance.Map<Course>(model);
            CourseCategory courseCategory= this.mapperProvider.Instance.Map<CourseCategory>(model);

            course.Category = courseCategory;
            course.MainImage = image;

            this.courseEfRepository.Add(course);
            this.dotLmsEfData.Commit();
        }

    }
}