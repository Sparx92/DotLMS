using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;

using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using DotLms.Web.Models.Backoffice;

namespace DotLms.Services.Data
{
    public class PageRetrivalService
    {
        private readonly IDotLmsEfData dotLmsEfData;
        private readonly IProjectableRepository<Page> pageProjectableRepository;
        private readonly IEntityFrameworkRepository<User> userRepository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapperProvider mapperProvider;

        public PageRetrivalService(IDotLmsEfData dotLmsEfData, IProjectableRepository<Page> pageProjectableRepository,
            IDateTimeProvider dateTimeProvider, IMapperProvider mapperProvider, IEntityFrameworkRepository<User> userRepository)
        {
            Guard.WhenArgument(dotLmsEfData, nameof(dotLmsEfData)).IsNull().Throw();
            Guard.WhenArgument(pageProjectableRepository, nameof(pageProjectableRepository)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(dateTimeProvider)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();
            Guard.WhenArgument(userRepository, nameof(userRepository)).IsNull().Throw();

            this.dotLmsEfData = dotLmsEfData;
            this.pageProjectableRepository = pageProjectableRepository;
            this.dateTimeProvider = dateTimeProvider;
            this.mapperProvider = mapperProvider;
            this.userRepository = userRepository;
        }

        public PageViewModel GetPage(string pageName)
        {
            var pageNameToLower = pageName.ToLowerInvariant();
            Page page = this.pageProjectableRepository
                .All
                .FirstOrDefault(x => x.UglyName == pageNameToLower);

            PageViewModel mapped = mapperProvider.Instance.Map<PageViewModel>(page);
            return mapped;
        }

        public PageViewModel GetPage(int? pageId)
        {
            Page page = this.pageProjectableRepository.All.FirstOrDefault(x => x.Id == pageId);
            PageViewModel mapped = mapperProvider.Instance.Map<PageViewModel>(page);
            return mapped;
        }

        public BackOfficeIndexViewModel GetAllPages()
        {
            var pages = this.pageProjectableRepository.All.ToList();
            var model = new BackOfficeIndexViewModel();
            model.Models = this.mapperProvider.Instance.Map<IEnumerable<PageViewModel>>(pages);
            return model;
        }
    }

}