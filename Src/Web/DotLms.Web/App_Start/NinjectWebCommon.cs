using System;
using System.Collections.Specialized;
using System.Web;

using DotLms.Data;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using DotLms.Services.Common;
using DotLms.Services.Common.Contracts;
using DotLms.Services.Data;
using DotLms.Services.Providers;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Identity.Managers;
using DotLms.Web.Interception;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;
using Ninject.Syntax;
using Ninject.Extensions.Interception.Infrastructure.Language;
using System.Runtime.Caching;
using DotLms.Services.Data.Contracts;
using DotLms.Web.Controllers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DotLms.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DotLms.Web.App_Start.NinjectWebCommon), "Stop")]

namespace DotLms.Web.App_Start
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
            var kernel = new StandardKernel();
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

            // Identity
            kernel.Bind<DotLmsSignInManager>().ToSelf().InRequestScope();
            kernel.Bind<DotLmsUserManager>().ToSelf().InRequestScope();
            kernel.Bind<IUserStore<User>>().To<DotLmsUserStore>().InRequestScope();

            // Data
            kernel.Bind<IDotLmsEfDbContext>().To<DotLmsEfDbContext>().InRequestScope();
            kernel.Bind<IDotLmsEfData>().To<DotLmsEfData>().InRequestScope();
            kernel.Bind(typeof(IEntityFrameworkRepository<>)).To(typeof(EntityFrameworkRepository<>)).InRequestScope();
            kernel.Bind(typeof(IProjectableRepository<>)).To(typeof(ProjectableRepository<>)).InRequestScope();

            // Proiders
            kernel.Bind<IDateTimeProvider>().To<DateTimeProvider>();
            kernel.Bind<IMapperProvider>().To<MapperProvider>().InSingletonScope();

            kernel.Bind<IMemoryCacheProvider>().To<MemoryCacheProvider>().InSingletonScope();
            kernel.Bind<MemoryCache>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument(typeof(string), "MyAwesomeCache")
                .WithConstructorArgument(typeof(NameValueCollection), new NameValueCollection());

            //Services      
            kernel.Bind<IProjectionService>().To<ProjectionService>().InRequestScope();

            kernel.Bind<ICourseCategoryService>().To<CourseCategoryService>()
                .WhenInjectedExactlyInto<CoursePresentationController>()
                .InRequestScope()
                .Intercept()
                .With<AllCourseCategoriesCacheingInterceptor>();

            kernel.Bind<ICourseService>().To<CourseService>()
                .WhenInjectedExactlyInto<CoursePresentationController>()
                .InRequestScope()
                .Intercept()
                .With<AllCoursesCacheingInterceptor>();

            kernel.Bind<ICourseCategoryService>().To<CourseCategoryService>().InRequestScope();
            kernel.Bind<ICourseService>().To<CourseService>().InRequestScope();
            kernel.Bind<IFileService>().To<FileService>().InRequestScope();
            kernel.Bind<IPageCreationService>().To<PageCreationService>().InRequestScope();
            kernel.Bind<IPageRetrivalService>().To<PageRetrivalService>().InRequestScope();
        }
    }
}
