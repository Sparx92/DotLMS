using DotLms.Web.Infrastructure;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWeb), "Start")]
namespace DotLms.Web.Infrastructure
{
    public static class NinjectWeb
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        }
    }
}