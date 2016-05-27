using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.SaveEvents;
using Sophcon.Data;
using Sophcon.Data.EntityFramework;
using System.Web.Http;
using DalEntities = CustomWise.Data.Entities;

namespace CustomWise.Web.Services.Controllers.Base {
    public class BaseController
        : ApiController {

        protected IConfigurationProvider AutoMapperConfigProvider { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        protected IUnitOfWork UnitOfWork { get; private set; }
        //protected IRepository<DalEntities.SpecificationVersion> SpecificationVersionRepository { get; private set; }
        protected IRepository<DalEntities.Specification> SpecificationRepository { get; private set; }
        protected IRepository<DalEntities.SpecificationType> SpecificationTypeRepository { get; private set; }
        //protected IRepository<DalEntities.MetaDataVersion> MetaDataVersionRepository { get; private set; }


        public BaseController(IDbContext dataContext, IConfigurationProvider autoMapperConfigProvier, IMapper mapper) {
            UnitOfWork = new EfUnitOfWork(dataContext);
            AutoMapperConfigProvider = autoMapperConfigProvier;
            AutoMapper = mapper;
            //SpecificationVersionRepository = new EfRepository<DalEntities.SpecificationVersion>(dataContext);
            SpecificationRepository = new EfRepository<DalEntities.Specification>(dataContext);
            SpecificationTypeRepository = new EfRepository<DalEntities.SpecificationType>(dataContext);
            //MetaDataVersionRepository = new EfRepository<DalEntities.MetaDataVersion>(dataContext);

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

        public static DataContextFactory CreateDataContextFactory() => new DataContextFactory();
    }
}
