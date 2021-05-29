using AutoMapper;
using System;
using System.Configuration;
using Unity;
using Unity.Injection;
using WebApplication1.Controllers.Infrastructure.AutoMapperProfile;
using WebApplication1.Repository.Helpers;
using WebApplication1.Repository.Implements;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.Implements;
using WebApplication1.Service.Infrastructure.AutoMapperProfile;
using WebApplication1.Service.Interfaces;

namespace WebApplication1
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // web config
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            container.RegisterType<IDatabaseHelper, DatabaseHelper>(new InjectionConstructor(connectionString));

            // Service
            container.RegisterType<INbaService, NbaService>();

            // Repository
            container.RegisterType<INbaRepository, NbaRepository>();

            // AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceProfile>();
                cfg.AddProfile<ControllerProfile>();
            });

            container.RegisterInstance(mapperConfig.CreateMapper());
        }
    }
}