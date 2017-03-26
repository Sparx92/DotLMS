using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bytes2you.Validation;

using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class PageCreationService : IPageCreationService
    {
        private readonly IDotLmsEfData dotLmsEfData;
        private readonly IProjectableRepository<Page> pageProjectableRepository;
        private readonly IEntityFrameworkRepository<User> userRepository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapperProvider mapperProvider;

        public PageCreationService(IDotLmsEfData dotLmsEfData, IProjectableRepository<Page> pageProjectableRepository,
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

            User author = this.userRepository.All.FirstOrDefault(x => x.UserName == username);
            Page mappedPage = this.mapperProvider.Instance.Map<Page>(model);
            model.HtmlContent = this.RemoveScriptTags(model.HtmlContent);

            if (model.ParentPage == null)
            {
                mappedPage.Author = author;
                mappedPage.LastEditedBy = author;
                mappedPage.CreatedOn = this.dateTimeProvider.UtcNow();
                mappedPage.LastEditedOn = this.dateTimeProvider.UtcNow();
                //TODO change to false in the future
                mappedPage.IsPublished = true;
                mappedPage.UglyName = this.GeneratUglyName(mappedPage.Name);
                mappedPage.Url = GenereateUrl(model.ParentCourse, mappedPage.UglyName);
            }
            this.pageProjectableRepository.Add(mappedPage);
            this.dotLmsEfData.SaveChanges();
        }

        private string GenereateUrl(CourseViewModel parent, string uglyName)
        {
            Guard.WhenArgument(uglyName, nameof(uglyName)).IsNull().Throw();

            if (parent == null)
            {
                string url = $"/{uglyName}";
                return url;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"/{parent.Url}");
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

            string uglyName = mappedPageName
                .ToLowerInvariant()
                .Replace(' ', '-');

            return uglyName;
        }

        private string RemoveScriptTags(string source)
        {
            return Regex.Replace(source, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
    }
}
