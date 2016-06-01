using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.SaveEvents;
using Sophcon.Data;
using Sophcon.Data.EntityFramework;
using Sophcon.Data.Infrastructure;
using System.Web.Http;
using DalEntities = CustomWise.Data.Entities;
using System;
using Sophcon;

namespace CustomWise.Web.Services.Controllers.Base {
    public class BaseController
        : ApiController {

        protected IConfigurationProvider AutoMapperConfigProvider { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        protected UnitOfWork UnitOfWork { get; private set; }
        //protected IRepository<DalEntities.SpecificationVersion> SpecificationVersionRepository { get; private set; }
        //protected Repository<DalEntities.Specification> SpecificationRepository { get; private set; }
        //protected Repository<DalEntities.SpecificationType> SpecificationTypeRepository { get; private set; }
        //protected IRepository<DalEntities.MetaDataVersion> MetaDataVersionRepository { get; private set; }


        public BaseController(IDbContext dataContext, IConfigurationProvider autoMapperConfigProvier, IMapper mapper) {
            UnitOfWork = new UnitOfWork(() => dataContext, new RepositoryFactory());
            AutoMapperConfigProvider = autoMapperConfigProvier;
            AutoMapper = mapper;
            //SpecificationVersionRepository = new EfRepository<DalEntities.SpecificationVersion>(dataContext);
            //SpecificationRepository = new Repository<DalEntities.Specification>(dataContext);
            //SpecificationTypeRepository = new Repository<DalEntities.SpecificationType>(dataContext);
            //MetaDataVersionRepository = new EfRepository<DalEntities.MetaDataVersion>(dataContext);

            UnitOfWork.RegisterSaveStrategy(new SetCreatedModifiedDataPreSaveEvent<UnitOfWork>(RequestContext.Principal.Identity.Name));
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

        public static DataContextFactory CreateDataContextFactory() => new DataContextFactory();
    }

    public class RepositoryFactory : IRepositoryFactory {
        public IRepository<TEntity> CreateInstance<TEntity>(IContext context) where TEntity : class, IEntity, new() {
            return new Repository<TEntity>(context as IDbContext);
        }
    }
}
