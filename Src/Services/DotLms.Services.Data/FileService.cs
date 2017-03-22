using System;
using System.IO;
using System.Web;
using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;

namespace DotLms.Services.Data
{
    public class FileService : IFileService
    {
        private readonly IDotLmsEfData dotLmsEfData;
        private readonly IEntityFrameworkRepository<MediaItem> mediaItemEfRepository;
        private readonly IMapperProvider mapperProvider;

        public FileService(IEntityFrameworkRepository<MediaItem> mediaItemEfRepository,
            IDotLmsEfData dotLmsEfData, IMapperProvider mapperProvider)
        {
            Guard.WhenArgument(mediaItemEfRepository, nameof(mediaItemEfRepository)).IsNull().Throw();
            Guard.WhenArgument(dotLmsEfData, nameof(dotLmsEfData)).IsNull().Throw();
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();

            this.mediaItemEfRepository = mediaItemEfRepository;
            this.dotLmsEfData = dotLmsEfData;
            this.mapperProvider = mapperProvider;
        }

        public MediaItemViewModel SaveFile(HttpPostedFileBase fileBase)
        {
            string extension = Path.GetExtension(fileBase.FileName);
            string uniqueFileName = Guid.NewGuid().ToString().Replace("-", "");
            string fullFileName = $"{uniqueFileName}{extension}";
            string relativePath = $"/Media/{fullFileName}";
            string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Media/"), fullFileName);

            MediaItem file = new MediaItem
            {
                Name = uniqueFileName,
                FullName = fullFileName,
                OriginalName = fileBase.FileName,
                FileType = fileBase.ContentType,
                Extension = extension,
                Url = relativePath
            };

            this.mediaItemEfRepository.Add(file);
            int writtenObjects = this.dotLmsEfData.SaveChanges();
            if (writtenObjects > 0)
            {
                fileBase.SaveAs(fullPath);
            }
            return this.mapperProvider.Instance.Map<MediaItemViewModel>(file);
        }
    }
}