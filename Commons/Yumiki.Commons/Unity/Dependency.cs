using CommonServiceLocator;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.ServiceLocation;

namespace Yumiki.Commons.Unity
{
    public class Dependency
    {
        private static Dependency service;
        /// <summary>
        /// Get all Register Type through config file (App.config or Web.config)
        /// </summary>
        /// <param name="containerName">Specific container name which defined in config file</param>
        /// <returns>Return DependencyHelper object in Singleton mode</returns>
        public static Dependency GetService(string containerName)
        {
            if (service == null)
            {
                service = new Dependency();
            }

            if (!string.IsNullOrWhiteSpace(containerName))
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
        public static Dependency GetService()
        {
            return GetService(string.Empty);
        }

        private UnityContainer container;
        private UnityServiceLocator locator;
        /// <summary>
        /// Constuctor for Helper to init UnityContainer and ServiceLocator of Unity IOC
        /// </summary>
        public Dependency()
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

        /// <summary>
        /// Resolve an instance of the default requested type from the container
        /// </summary>
        /// <returns>Return an object from type</returns>
        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}
