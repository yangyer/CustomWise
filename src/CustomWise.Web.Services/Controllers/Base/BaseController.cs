using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.Events;
using Sophcon.Data;
using Sophcon.Data.EntityFramework;
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
        protected RepositoryBase<DalEntities.SpecificationVersion, IDbContext> SpecificationVersionRepository { get; private set; }
        protected RepositoryBase<DalEntities.Specification, IDbContext> SpecificationRepository { get; private set; }
        protected RepositoryBase<DalEntities.SpecificationType, IDbContext> SpecificationTypeRepository { get; private set; }

        public BaseController(IUnitOfWork unitOfWork, IConfigurationProvider autoMapperConfigProvier, IMapper mapper) {
            var dataContextFactory = DataContextFactory.CreateDataContextFactory();
            UnitOfWork = unitOfWork;
            AutoMapperConfigProvider = autoMapperConfigProvier;
            AutoMapper = mapper;
            SpecificationVersionRepository = new EfRepository<DalEntities.SpecificationVersion>(dataContextFactory.GetContext());
            SpecificationRepository = new EfRepository<DalEntities.Specification>(dataContextFactory.GetContext());
            SpecificationTypeRepository = new EfRepository<DalEntities.SpecificationType>(dataContextFactory.GetContext());
            UnitOfWork.RegisterPreSave(new SetCreatedModifiedDataPreSaveEvent(RequestContext.Principal.Identity.Name));
        }
    }

    public class DataContextFactory {

        private IDbContext _contextInstance;

        public DataContextFactory() {
            _contextInstance = new CustomWiseModel();
        }

        public void Dispose() {
            _contextInstance.Dispose();
            _contextInstance = null;
        }

        public IDbContext GetContext() {
            return _contextInstance;
        }

        private static DataContextFactory _dataContextFactory;
        public static DataContextFactory CreateDataContextFactory() => _dataContextFactory ?? (_dataContextFactory = new DataContextFactory());
    }
}
