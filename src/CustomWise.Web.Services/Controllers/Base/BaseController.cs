using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.Events;
using Sophcon.Data;
using Sophcon.Data.EntityFramework;
using Sophcon.Data.EntityFramework.Versioning;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web.Http;
using DalEntities = CustomWise.Data.Entities;

namespace CustomWise.Web.Services.Controllers.Base {
    public class BaseController
        : ApiController {

        protected IConfigurationProvider AutoMapperConfigProvider { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected VersionRepository<DalEntities.Specification, DalEntities.SpecificationVersion, CustomWiseModel> SpecificationRepository { get; private set; }
        protected IRepository<DalEntities.SpecificationType> SpecificationTypeRepository { get; private set; }

        public BaseController(IUnitOfWork unitOfWork, IConfigurationProvider autoMapperConfigProvier, IMapper mapper) {
            UnitOfWork = unitOfWork;
            AutoMapperConfigProvider = autoMapperConfigProvier;
            AutoMapper = mapper;
            SpecificationRepository = new VersionRepository<DalEntities.Specification, DalEntities.SpecificationVersion, CustomWiseModel>(
                CustomWiseServiceLocator.CreateServiceLocator(),
                (ev) => new DalEntities.Specification {
                    Id = ev.Id,
                    ArtifactReferenceId = ev.ArtifactReferenceId,
                    DisplayName = ev.DisplayName,
                    IsActive = ev.IsActive,
                    ItemTypeId = ev.ItemTypeId,
                    Order = ev.Order,
                    ParentId = ev.ParentId,
                    CreatedBy = ev.CreatedBy,
                    CreatedDate = ev.CreatedDate,
                    ModifiedBy = ev.ModifiedBy,
                    ModifiedDate = ev.ModifiedDate
                });

            UnitOfWork.RegisterPreSave(new SetCreatedModifiedDataPreSaveEvent(RequestContext.Principal.Identity.Name));
        }
    }

    public class CustomWiseServiceLocator
        : IServiceLocator {

        private ConcurrentDictionary<Type, object> _services;

        public CustomWiseServiceLocator() {
            _services = new ConcurrentDictionary<Type, object> {
                [typeof(IDataContextFactory<CustomWiseModel>)] = new DataContextFactory()
            };
        }

        public TServiceType GetServiceOfType<TServiceType>() 
            where TServiceType : class {

            if (_services.ContainsKey(typeof(TServiceType))) {
                return (TServiceType)_services[typeof(TServiceType)];
            }

            throw new NotSupportedException();
        }

#warning: Need to DI this into the sub controllers but serving it up here as a singleton for now until DI is implemented.
        private static IServiceLocator _singletonServiceLocator;
        public static IServiceLocator CreateServiceLocator() {
            return _singletonServiceLocator ?? (_singletonServiceLocator = new CustomWiseServiceLocator());
        }
    }

    public class DataContextFactory 
        : IDataContextFactory<CustomWiseModel> {

        private CustomWiseModel _contextInstance;

        public DataContextFactory() {
            _contextInstance = new CustomWiseModel();
        }

        public void Dispose() {
            _contextInstance.Dispose();
            _contextInstance = null;
        }

        public CustomWiseModel GetContext() {
            return _contextInstance;
        }
    }
}
