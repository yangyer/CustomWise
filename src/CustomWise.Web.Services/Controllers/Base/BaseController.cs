using AutoMapper;
using System.Web.Http;

namespace CustomWise.Web.Services.Controllers.Base {
    public class BaseController
        : ApiController {

        protected IConfigurationProvider AutoMapperConfigProvier { get; private set; }

        public BaseController(IConfigurationProvider autoMapperConfigProvier) {
            AutoMapperConfigProvier = autoMapperConfigProvier;
        }
    }
}
