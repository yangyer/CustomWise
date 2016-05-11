using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.Events;
using Sophcon.Data;
using Sophcon.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Web.Http;
using DalEntities = CustomWise.Data.Entities;

namespace CustomWise.Web.Services.Controllers.Base {
    public class BaseController
        : ApiController {

        protected IConfigurationProvider AutoMapperConfigProvider { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected IRepository<DalEntities.Specification> SpecificationRepository { get; private set; }
        protected IRepository<DalEntities.SpecificationType> SpecificationTypeRepository { get; private set; }

        public BaseController(IUnitOfWork unitOfWork, IConfigurationProvider autoMapperConfigProvier, IMapper mapper) {
            UnitOfWork = unitOfWork;
            AutoMapperConfigProvider = autoMapperConfigProvier;
            AutoMapper = mapper;
            SpecificationRepository = new Repository<DalEntities.Specification>(CustomWiseServiceLocator.CreateServiceLocator());

            UnitOfWork.RegisterPreSave(new SetCreatedModifiedDataPreSaveEvent(RequestContext.Principal.Identity.Name));
        }
    }

#warning: Need to DI this into the sub controllers but serving it up here as a singleton for now until DI is implemented.
    public class CustomWiseServiceLocator
        : IServiceLocator {

        private Dictionary<Type, object> _services;

        public CustomWiseServiceLocator() {
            _services.Add(typeof(DataContextFactory), new DataContextFactory());
        }

        public TServiceType GetServiceOfType<TServiceType>() 
            where TServiceType : class {

            if (_services.ContainsKey(typeof(TServiceType))) {
                return (TServiceType)_services[typeof(TServiceType)];
            }

            throw new NotSupportedException();
        }

        public static IServiceLocator CreateServiceLocator() {
            return new CustomWiseServiceLocator();
        }
    }

    public class DataContextFactory : IDataContextFactory<ICustomWiseContext> {

        private ICustomWiseContext _contextInstance;

        public void Dispose() {
            _contextInstance.Dispose();
            _contextInstance = null;
        }

        public ICustomWiseContext GetContext() {
            return _contextInstance ?? (_contextInstance = new CustomWiseModel());
        }
    }
}
