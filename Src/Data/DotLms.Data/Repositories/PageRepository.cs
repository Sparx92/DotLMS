using System.Collections.Generic;
using System.Linq;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Web.Models;

namespace DotLms.Data.Repositories
{
    public class PageRepository : GenericRepository<Page>
    {
        public PageRepository(IDotLmsDbContext context) : base(context)
        {
        }

        //public IEnumerable<Page> GetPageTree()
        //{
        //    this.All.Select(p => new
        //    {
        //        p.Id,
        //        p.ParentPage,
        //        p.ChildPages,
        //        p.Name,
        //        p.IsPublished,
        //        Author = p.Author.UserName,
        //        p.CreatedOn,
        //        p.LastEditedOn,
        //        LastEditor = p.LastEditedBy.UserName,
        //        p.DeletedOn,
        //        p.UglyName,
        //        p.Url
        //    })
        //    .AsEnumerable()
        //    .Select(p=>new PageWebModel
        //        {
        //            LastEditedOn = p.LastEditedOn,
        //            ParentPage = p.ParentPage,
        //            ChildPages = p.ChildPages,
                    
        //        });
        //}
    }
}