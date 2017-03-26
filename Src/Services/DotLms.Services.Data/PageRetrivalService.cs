using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;

using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using DotLms.Web.Models.Backoffice;

namespace DotLms.Services.Data
{
    public class PageRetrivalService : IPageRetrivalService
    {
        private readonly IProjectableRepository<Page> pageProjectableRepository;
        private readonly IMapperProvider mapperProvider;

        public PageRetrivalService(
            IProjectableRepository<Page> pageProjectableRepository,
            IMapperProvider mapperProvider)
        {
            Guard.WhenArgument(pageProjectableRepository, nameof(pageProjectableRepository)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();

            this.pageProjectableRepository = pageProjectableRepository;
            this.mapperProvider = mapperProvider;
        }

        public PageViewModel GetPage(string pageName)
        {
            Guard.WhenArgument(pageName,nameof(pageName)).IsNullOrEmpty().Throw();
            
            string pageNameToLower = pageName.ToLowerInvariant();
            Page page = this.pageProjectableRepository
                .All
                .FirstOrDefault(x => x.UglyName == pageNameToLower);

            PageViewModel mapped = mapperProvider.Instance.Map<PageViewModel>(page);
            return mapped;
        }

        public PageViewModel GetPage(int pageId)
        {
            Page page = this.pageProjectableRepository.All.FirstOrDefault(x => x.Id == pageId);
            PageViewModel mapped = mapperProvider.Instance.Map<PageViewModel>(page);
            return mapped;
        }

        public BackOfficeIndexViewModel GetAllPages()
        {
            List<Page> pages = this.pageProjectableRepository.All.ToList();
            BackOfficeIndexViewModel model = new BackOfficeIndexViewModel();
            model.Models = this.mapperProvider.Instance.Map<IEnumerable<PageViewModel>>(pages);
            return model;
        }
    }
}