using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.Base;
using DalEntities = CustomWise.Data.Entities;
using DtoEntities = CustomWise.Web.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/specification")]
    public class SpecificationController
        : BaseController {
       
        public SpecificationController()
            : base(new CustomWiseModel(), AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperMapperInstance()) {
            Context.RegisterPreSave((entityList, state) => {
                entityList.ToList().ForEach(entity => {
                    entity.CreatedBy = entity.State == Sophcon.SophconEntityState.Added ? RequestContext.Principal.Identity.Name : entity.CreatedBy;
                    entity.CreatedDate = entity.State == Sophcon.SophconEntityState.Added ? DateTime.Now : entity.CreatedDate;
                    entity.ModifiedBy = RequestContext.Principal.Identity.Name;
                    entity.ModifiedDate = DateTime.Now;
                });
            });
        }

        public SpecificationController(ICustomWiseContext context, IConfigurationProvider autoMapperConfigProvier, IMapper mapper)
            : base(context, autoMapperConfigProvier, mapper) {
        }

        [Route]
        public async Task<IEnumerable<DtoEntities.Specification>> Get() {
            var specifications = await Context.Specifications.ToListAsync();

            return AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications);
        }

        [Route("{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id) {
            var specification = await SpecificationRepository.FindAsync(id);
            return AutoMapper.Map<DtoEntities.Specification>(specification);
        }

        [Route]
        public async Task<int> Post(DtoEntities.Specification specification, bool published = false) {
            throw new NotImplementedException();
        }

        [Route]
        public async Task<int> Put(DtoEntities.Specification specification, bool published = false) {
            throw new NotImplementedException();
        }
    }
}
