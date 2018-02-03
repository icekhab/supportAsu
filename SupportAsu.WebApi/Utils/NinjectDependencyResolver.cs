using DomainController.Manager;
using Ninject;
using SupportAsu.Claim.Services;
using SupportAsu.Claim.Services.Abstract;
using SupportAsu.Dictionary.Manager;
using SupportAsu.Dictionary.Manager.Abstract;
using SupportAsu.Dictionary.Services;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupportAsu.Task.Service;
using SupportAsu.Task.Service.Abstract;
using UserManagment.Managers;
using UserManagment.Providers;

namespace SupportAsu.WebApi.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IDomainManager>().To<DomainManager>().InSingletonScope();


            kernel.Bind<IUserManager>().To<UserManager>();


            kernel.Bind<IGenericRepository>().To<GenericRepository>();
            
            kernel.Bind<ITaskService>().To<TaskService>();
            #region Dictionary
            kernel.Bind<IDictionaryManager>().To<DictionaryManager>().InSingletonScope();
            kernel.Bind<IDictionaryService>().To<DictionaryService>();
            kernel.Get<IDictionaryManager>().Initialize();
            #endregion

            #region Claim
            kernel.Bind<IClaimService>().To<ClaimService>();
            #endregion
        }
    }
}