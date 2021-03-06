[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FourWheels.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FourWheels.Web.App_Start.NinjectWebCommon), "Stop")]

namespace FourWheels.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using System.Data.Entity;
    using FourWheels.Data.DbContexts;
    using FourWheels.Data.Repositories;
    using FourWheels.Data.UnitOfWork;
    using AutoMapper;
    using FourWheels.Services.Contracts;
    using FourWheels.Services;

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
            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IService))
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(typeof(DbContext), typeof(FourWheelsSqlDbContext)).To<FourWheelsSqlDbContext>().InRequestScope();
            //kernel.Bind<IFourWheelsSqlDbContext>().To<FourWheelsSqlDbContext>().InRequestScope();
            kernel.Bind(typeof(IEfRepostory<>)).To(typeof(EfRepostory<>));
            kernel.Bind<IEfUnitOfWork>().To<EfUnitOfWork>().InRequestScope();
            kernel.Bind<IMapper>().ToMethod(x => Mapper.Instance).InSingletonScope();

            kernel.Bind<ICarFeatureServices>().To<CarFeaturesServices>().InRequestScope();
            //kernel.Bind<ICarServices>().To<CarServices>().InRequestScope();

        }
    }
}
