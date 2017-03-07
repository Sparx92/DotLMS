using System;
using System.Web;

using DotLms.Data;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using DotLms.Services.Providers;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Identity.Managers;
using DotLms.Web.Infrastructure;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace DotLms.Web.Infrastructure
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAuthenticationManager>().ToMethod(
                  c =>
                      HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            kernel.Bind<IDotLmsDbContext>().To<DotLmsDbContext>().InRequestScope();
            kernel.Bind<DotLmsSignInManager>().ToSelf().InRequestScope();
            kernel.Bind<DotLmsUserManager>().ToSelf().InRequestScope();
            kernel.Bind<IUserStore<User>>().To<DotLmsUserStore>().InRequestScope();

            kernel.Bind<IDotLmsData>().To<DotLmsData>().InRequestScope();
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>)).InRequestScope();

            kernel.Bind<IDateTimeProvider>().To<DateTimeProvider>();
        }        
    }
}
