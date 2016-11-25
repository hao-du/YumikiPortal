using Yumiki.Commons.Unity;

namespace Yumiki.Business.Base
{
    /// <summary>
    /// Parent class for all Business Logic Layer classes to contains all common properties and methods.
    /// </summary>
    public class BaseService<T>
    {
        private T repository;
        protected T Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = Service.GetInstance<T>();
                }
                return repository;
            }
        }

        private Dependency service;
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
        protected Dependency Service
        {
            get
            {
                if (service == null)
                {
                    //Get domain name such as "SampleService" in "Yumiki.Business.SampleService" (index = 2)
                    string containerName = this.GetType().FullName.Split('.')[2];
                    service = Dependency.GetService(containerName);
                }
                return service;
            }
        }
    }
}
