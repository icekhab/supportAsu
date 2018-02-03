using DomainController.Manager;
using Ninject;
using SupportAsu.Claim.Services;
using SupportAsu.Claim.Services.Abstract;
using SupportAsu.Dictionary.Manager;
using SupportAsu.Dictionary.Manager.Abstract;
using SupportAsu.Dictionary.Services;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.Repository;
using SupportAsu.Task.Service;
using SupportAsu.Task.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Equipments.Service;
using Equipments.Service.Abstract;
using SupportAsu.ProjectorShedule.Services;
using SupportAsu.ProjectorShedule.Services.Abstract;
using UserManagment.Managers;
using SupportAsu.Prophylaxis.Services.Abstract;
using SupportAsu.Prophylaxis.Services;
using SupportAsu.User.Services.Abstract;
using SupportAsu.User.Services;

namespace SupportAsu.Web.Utils
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


            #region Dictionary
            kernel.Bind<IDictionaryManager>().To<DictionaryManager>().InSingletonScope();
            kernel.Bind<IDictionaryService>().To<DictionaryService>();
            kernel.Get<IDictionaryManager>().Initialize();
            #endregion

            #region Claim
            kernel.Bind<IClaimService>().To<ClaimService>();
            #endregion

            #region Task
            kernel.Bind<ITaskService>().To<TaskService>();
            #endregion

            #region Equipment

            kernel.Bind<IEquipmentService>().To<EquipmentService>(); 
            kernel.Bind<IPlanPurchaseService>().To<PlanPurchaseService>();

            #endregion

            #region ProjectorShedule

            kernel.Bind<IProjectorSheduleService>().To<ProjectorSheduleService>();

            #endregion


            #region Prophylaxis

            kernel.Bind<IProphylaxisService>().To<ProphylaxisService>();
            #endregion

            #region User
            kernel.Bind<IUserService>().To<UserService>();
            #endregion
        }
    }
}