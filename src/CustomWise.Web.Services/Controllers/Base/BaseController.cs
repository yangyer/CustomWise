using AutoMapper;
using CustomWise.Data;
using Sophcon.Data.EntityFramework;
using System.Web.Http;
using DalEntities = CustomWise.Data.Entities;

namespace CustomWise.Web.Services.Controllers.Base {
    public class BaseController
        : ApiController {

        protected IConfigurationProvider AutoMapperConfigProvider { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        protected ICustomWiseContext Context { get; private set; }
        protected Repository<DalEntities.Specification> SpecificationRepository { get; set; }
        protected Repository<DalEntities.SpecificationType> SpecificationTypeRepository { get; set; }
        protected Repository<DalEntities.Artifact> ArtifactRepository { get; set; }

        public BaseController(ICustomWiseContext context, IConfigurationProvider autoMapperConfigProvier, IMapper mapper) {
            Context = context;
            AutoMapperConfigProvider = autoMapperConfigProvier;
            AutoMapper = mapper;
        }
    }
}
