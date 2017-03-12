using System;
using System.Web;

using DotLms.Data;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using DotLms.Services.Common;
using DotLms.Services.Common.Contracts;
using DotLms.Services.Providers;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Identity.Managers;
using DotLms.Web.Infrastructure;
using DotLms.Web.Infrastructure.Mappings;
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

        public static IKernel Kernel { get; private set; }

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
            StandardKernel kernel = new StandardKernel();
            Kernel = kernel;

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

            kernel.Bind<IDotLmsEfDbContext>().To<DotLmsEfDbContext>().InRequestScope();
            kernel.Bind<DotLmsSignInManager>().ToSelf().InRequestScope();
            kernel.Bind<DotLmsUserManager>().ToSelf().InRequestScope();
            kernel.Bind<IUserStore<User>>().To<DotLmsUserStore>().InRequestScope();

            kernel.Bind<IDotLmsEfData>().To<DotLmsEfData>().InRequestScope();
            kernel.Bind(typeof(IEntityFrameworkRepository<>)).To(typeof(EntityFrameworkRepository<>)).InRequestScope();
            kernel.Bind(typeof(IProjectableRepository<>)).To(typeof(ProjectableRepository<>)).InRequestScope();
            kernel.Bind<IProjectionService>().To<ProjectionService>().InRequestScope();

            kernel.Bind<IDateTimeProvider>().To<DateTimeProvider>();
            kernel.Bind<IMapperProvider>().To<MapperProvider>().InSingletonScope();
        }        
    }
}
