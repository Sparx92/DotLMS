using System;
using System.IO;
using System.Web;
using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Data.Models;

namespace DotLms.Services.Data
{
    public class FileService
    {
        private IDotLmsEfData dotLmsEfData;
        private IEntityFrameworkRepository<MediaItem> mediaItemEfRepository;

        public FileService(IEntityFrameworkRepository<MediaItem> mediaItemEfRepository, IDotLmsEfData dotLmsEfData)
        {
            Guard.WhenArgument(mediaItemEfRepository, nameof(mediaItemEfRepository)).IsNull().Throw();
            Guard.WhenArgument(dotLmsEfData, nameof(dotLmsEfData)).IsNull().Throw();

            this.mediaItemEfRepository = mediaItemEfRepository;
            this.dotLmsEfData = dotLmsEfData;
        }

        public MediaItem SaveFile(HttpPostedFileBase fileBase)
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
            return file;
        }
    }
}