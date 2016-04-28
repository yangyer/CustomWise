using AutoMapper;
using System.Web.Http;

namespace CustomWise.Web.Services.Controllers.Base {
    public class BaseController
        : ApiController {

        protected IConfigurationProvider AutoMapperConfigProvider { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        public BaseController(IConfigurationProvider autoMapperConfigProvier, IMapper mapper) {
            AutoMapperConfigProvider = autoMapperConfigProvier;
            AutoMapper = mapper;
        }
    }
}
