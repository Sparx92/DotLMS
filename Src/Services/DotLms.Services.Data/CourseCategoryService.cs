using System.Collections.Generic;
using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly IEntityFrameworkRepository<CourseCategory> categoryRepository;
        private readonly IProjectableRepository<CourseCategory> categoryProjectableRepository;
        private readonly IDotLmsEfData dotLmsEfData;
        private readonly IMapperProvider mapperProvider;

        public CourseCategoryService(IEntityFrameworkRepository<CourseCategory> categoryRepository,
            IDotLmsEfData dotLmsEfData, IMapperProvider mapperProvider,
            IProjectableRepository<CourseCategory> categoryProjectableRepository)
        {
            Guard.WhenArgument(categoryRepository, nameof(categoryRepository)).IsNull().Throw();
            Guard.WhenArgument(dotLmsEfData, nameof(dotLmsEfData)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();
            Guard.WhenArgument(categoryProjectableRepository, nameof(categoryProjectableRepository)).IsNull().Throw();

            this.categoryRepository = categoryRepository;
            this.dotLmsEfData = dotLmsEfData;
            this.mapperProvider = mapperProvider;
            this.categoryProjectableRepository = categoryProjectableRepository;
        }

        public void CreateNewCategory(CourseCategoryViewModel model)
        {
            Guard.WhenArgument(model,nameof(model)).IsNull().Throw();
            CourseCategory category = this.mapperProvider.Instance.Map<CourseCategory>(model);

            this.categoryRepository.Add(category);
            this.dotLmsEfData.SaveChanges();
        }

        public CourseCategoryViewModel GetCategoryViewModel(string name)
        {
            return this.categoryProjectableRepository.GetFirstMapped<CourseCategoryViewModel>(x => x.Name == name);
        }

        public IEnumerable<CourseCategoryViewModel> GetAllCategories()
        {
            return this.categoryProjectableRepository.GetAllMapped<CourseCategoryViewModel>();
        }
    }
}