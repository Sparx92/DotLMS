using System;
using System.Collections.Generic;
using System.Text;

using Bytes2you.Validation;

using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class PageCreationService
    {
        private readonly IDotLmsData dotLmsData;
        private readonly IProjectableRepository<Page> pageProjectableRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapperProvider mapperProvider;

        public PageCreationService(IDotLmsData dotLmsData, IProjectableRepository<Page> pageProjectableRepository,
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

        public IEnumerable<PageViewModel> GetAllPages()
        {
            return this.pageProjectableRepository.GetAllMapped<PageViewModel>();
        }

        /// <summary>
        /// Creates a front page
        /// </summary>
        /// <param name="model">The Model sent back from the form</param>
        /// <param name="username">The user invoking the method</param>
        public void CreatePage(PageViewModel model, string username)
        {
            Guard.WhenArgument(model, nameof(model)).IsNull().Throw();
            Guard.WhenArgument(username, nameof(username)).IsNull().Throw();

            User author = this.userRepository.GetFirst(x => x.UserName == username);
            var mappedPage = this.mapperProvider.Instance.Map<Page>(model);

            if (model.ParentPage == null)
            {
                mappedPage.Author = author;
                mappedPage.LastEditedBy = author;
                mappedPage.CreatedOn = this.dateTimeProvider.UtcNow();
                mappedPage.LastEditedOn = this.dateTimeProvider.UtcNow();
                //TODO change to false in the future
                mappedPage.IsPublished = true;
                mappedPage.UglyName = this.GeneratUglyName(mappedPage.Name);
                mappedPage.Url = this.GenereateUrl(mappedPage.ParentPage, mappedPage.UglyName);
            }
            this.pageProjectableRepository.Add(mappedPage);
            this.dotLmsData.Commit();
        }

        private string GenereateUrl(Page parentPage, string uglyName)
        {
            Guard.WhenArgument(uglyName, nameof(uglyName)).IsNull().Throw();

            if (parentPage == null)
            {
                string url = $"/{uglyName}";
                return url;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"/{parentPage.Url}");
                stringBuilder.Append($"/{uglyName}");
                return stringBuilder.ToString();
            }
        }

        private string GeneratUglyName(string mappedPageName)
        {
            if (mappedPageName.IndexOf(" ", StringComparison.Ordinal) < 0)
            {
                return mappedPageName;
            }

            var uglyName = mappedPageName
                .ToLowerInvariant()
                .Replace(' ', '-');

            return uglyName;
        }
    }
}
