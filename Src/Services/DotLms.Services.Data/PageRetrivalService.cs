using System.Collections.Generic;
using Bytes2you.Validation;

using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class PageRetrivalService
    {
        private readonly IDotLmsData dotLmsData;
        private readonly IProjectableRepository<Page> pageProjectableRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapperProvider mapperProvider;

        public PageRetrivalService(IDotLmsData dotLmsData, IProjectableRepository<Page> pageProjectableRepository,
            IDateTimeProvider dateTimeProvider, IMapperProvider mapperProvider, IGenericRepository<User> userRepository)
        {
            Guard.WhenArgument(dotLmsData, nameof(dotLmsData)).IsNull().Throw();
            Guard.WhenArgument(pageProjectableRepository, nameof(pageProjectableRepository)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(dateTimeProvider)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();
            Guard.WhenArgument(userRepository, nameof(userRepository)).IsNull().Throw();

            this.dotLmsData = dotLmsData;
            this.pageProjectableRepository = pageProjectableRepository;
            this.dateTimeProvider = dateTimeProvider;
            this.mapperProvider = mapperProvider;
            this.userRepository = userRepository;
        }

        public PageViewModel GetPage(string pageName)
        {
            var pageNameToLower = pageName.ToLowerInvariant();
            Page page = this.pageProjectableRepository.GetFirst(x => x.UglyName == pageNameToLower);
            PageViewModel mapped = mapperProvider.Instance.Map<PageViewModel>(page);
            return mapped;
        }

        public PageViewModel GetPage(int? pageId)
        {
            Page page = this.pageProjectableRepository.GetById(pageId);
            PageViewModel mapped = mapperProvider.Instance.Map<PageViewModel>(page);
            return mapped;
        }

        public BackOfficeIndexViewModel GetAllPages()
        {
            var pages = this.pageProjectableRepository.GetAll();
            var model = new BackOfficeIndexViewModel();
            model.Models= this.mapperProvider.Instance.Map<IEnumerable<PageViewModel>>(pages);
            return model;
        }
    }

}