using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MyConsole
{
    /// <summary>
    /// 容器的工厂，单例容器构造
    /// </summary>
    public class ContainerFactory
    {
        private static IUnityContainer container = null;
        static ContainerFactory()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
            container = new UnityContainer();
            section.Configure(container, "lcContainer");
        }

        public static IUnityContainer GetContainer()
        {
            return container;
        }
    }
}
