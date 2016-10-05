using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Business.Base
{
    public class DependencyHelper
    {
        private static DependencyHelper service;
        /// <summary>
        /// Get all Register Type through config file (App.config or Web.config)
        /// </summary>
        /// <param name="containerName">Specific container name which defined in config file</param>
        /// <returns>Return DependencyHelper object in Singleton mode</returns>
        public static DependencyHelper GetService(string containerName)
        {
            if (service == null)
            {
                service = new DependencyHelper();

            }

            if (!string.IsNullOrEmpty(containerName))
            {
                service.container.LoadConfiguration(containerName);
            }
            else
            {
                service.container.LoadConfiguration();
            }

            return service;
        }

        /// <summary>
        /// Get all Register Type through config file (App.config or Web.config)
        /// </summary>
        /// <returns>Return DependencyHelper object in Singleton mode</returns>
        public static DependencyHelper GetService()
        {
            return GetService(string.Empty);
        }

        private UnityContainer container;
        private UnityServiceLocator locator;
        /// <summary>
        /// Constuctor for Helper to init UnityContainer and ServiceLocator of Unity IOC
        /// </summary>
        public DependencyHelper()
        {
            container = new UnityContainer();
            locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        /// <summary>
        /// Get Actual Instance of Class through an Interface
        /// </summary>
        /// <typeparam name="T">Delegation type for a specific type</typeparam>
        /// <returns>Return a specific type through param T</returns>
        public T GetInstance<T>()
        {
            T instance = ServiceLocator.Current.GetInstance<T>();
            return instance;
        }
    }
}
