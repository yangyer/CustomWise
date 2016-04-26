using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CustomWise.Web.Services.Models;

namespace CustomWise.Web.Services.Controllers {

    [RoutePrefix("customwise/api/specifications")]
    public class SpecificationController
        : ApiController {

        [Route(Name = "Get")]
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
