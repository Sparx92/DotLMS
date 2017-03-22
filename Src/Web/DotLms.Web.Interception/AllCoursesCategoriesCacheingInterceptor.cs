using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DotLms.Services.Providers.Contracts;
using Ninject.Extensions.Interception;

namespace DotLms.Web.Interception
{
    public class AllCourseCategoriesCacheingInterceptor : IInterceptor
    {
        private IMemoryCacheProvider memoryCacheProvider;

        public AllCourseCategoriesCacheingInterceptor(IMemoryCacheProvider memoryCacheProvider)
        {
            this.memoryCacheProvider = memoryCacheProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Method.Name == "GetAllCategories")
            {
                string name = "AllCategories";

                if (this.memoryCacheProvider.MemoryCache.Get(name) == null)
                {
                    invocation.Proceed();
                    if (invocation.ReturnValue != null)
                    {
                        this.memoryCacheProvider.MemoryCache.Add(name, invocation.ReturnValue,
                            Common.DateTimeVariables.TenMinutesFromUtcNow);
                    }
                }
                else
                {
                    invocation.ReturnValue = this.memoryCacheProvider.MemoryCache.Get(name);
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
