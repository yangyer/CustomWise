using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CustomWise.Web.Services.Models;
using CustomWise.Web.Services.Controllers.Base;
using AutoMapper;

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/recordtype")]
    public class SpecificationController
        : BaseController {

        public SpecificationController()
            : base(AutoMapperFactory.CreateAutoMapperConfigProviderInstance()) {
        }

        public SpecificationController(IConfigurationProvider autoMapperConfigProvier)
            : base(autoMapperConfigProvier) {
        }

        [Route]
        public async Task<IEnumerable<Specification>> Get() {
            throw new NotImplementedException();
        }

        [Route("{id:int}")]
        public async Task<Specification> Get(int id) {
            // return await Task.Run(() => new Specification { Id = id });
            throw new NotImplementedException();
        }
    }
}
