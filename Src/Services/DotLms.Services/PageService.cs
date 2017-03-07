using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Data.Models;

namespace DotLms.Services
{
    public class PageService
    {
        private IDotLmsData dotLmsData;
        private IGenericRepository<Page> pageGenericRepository;

        public PageService(IDotLmsData dotLmsData, IGenericRepository<Page> pageGenericRepository)
        {
            Guard.WhenArgument(dotLmsData, nameof(dotLmsData)).IsNull().Throw();
            Guard.WhenArgument(pageGenericRepository, nameof(pageGenericRepository)).IsNull().Throw();

            this.dotLmsData = dotLmsData;
            this.pageGenericRepository = pageGenericRepository;
        }

        public void CreatePage()
        {
            this.dotLmsData.Commit();
        }

       
    }
}
