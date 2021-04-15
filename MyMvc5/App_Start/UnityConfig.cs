using EF.Business.IService;
using EF.Business.IService.ICombinationService;
using EF.Business.Service;
using EF.Business.Service.CombinationService;
using EF.Model;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MyMvc5
{
    public static class UnityConfig
    {
        #region MyRegion
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterContainer(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void RegisterContainer(UnityContainer container)
        {
            //
            container.RegisterType<DbContext, LCDbContext>();

            //非组合服务注册
            container.RegisterType<ITb_DebugLogService, Tb_DebugLogService>();
            container.RegisterType<ITb_ErrorService, Tb_ErrorService>();
            container.RegisterType<ITb_LogService, Tb_LogService>();
            container.RegisterType<ITb_MqService, Tb_MqService>();

            //组合服务注册
            container.RegisterType<IMqLogService, MqLogService>();
        }
        #endregion

        ////单例模式
        //private static UnityContainer container = new UnityContainer();
        //public static void RegisterComponents()
        //{
        //    //var container = new UnityContainer();

        //    // register all your components with the container here
        //    // it is NOT necessary to register your controllers

        //    // e.g. container.RegisterType<ITestService, TestService>();
        //    RegisterContainer(container);

        //    DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        //}

        //private static void RegisterContainer(UnityContainer container)
        //{
        //    //
        //    container.RegisterType<DbContext, LCDbContext>();

        //    //非组合服务注册
        //    container.RegisterType<ITb_DebugLogService, Tb_DebugLogService>();
        //    container.RegisterType<ITb_ErrorService, Tb_ErrorService>();
        //    container.RegisterType<ITb_LogService, Tb_LogService>();
        //    container.RegisterType<ITb_MqService, Tb_MqService>();

        //    //组合服务注册
        //    container.RegisterType<IMqLogService, MqLogService>();
        //}

        //public static IUnityContainer GetContainer()
        //{
        //    return container;
        //}

    }
}