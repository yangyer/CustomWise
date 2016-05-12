using AutoMapper;
using CustomWise.Web.Services.Controllers.Base;
using Sophcon.Data;
using Sophcon.Data.EntityFramework;
using Sophcon.Data.EntityFramework.Versioning;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using DtoEntities = CustomWise.Web.Services.Models;

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/specification")]
    public class SpecificationController
        : BaseController {
        
        public SpecificationController()
            : base(new VersionUnitOfWork(CustomWiseServiceLocator.CreateServiceLocator()), AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperMapperInstance()) {
        }

        public SpecificationController(IUnitOfWork unitOfWork, IConfigurationProvider autoMapperConfigProvier, IMapper mapper)
            : base(unitOfWork, autoMapperConfigProvier, mapper) {
        }

        [Route]
        public async Task<IEnumerable<DtoEntities.Specification>> Get() {
            var specifications = await SpecificationRepository.Get().ToListAsync();

            return AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications);
        }

        [Route("{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id) {
            var specification = await SpecificationRepository.FindAsync(id);
            return AutoMapper.Map<DtoEntities.Specification>(specification);
        }

        [Route("{version:int}/{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id, int version) {
            var specification = await SpecificationRepository.FindVersionAsync(version, id);
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
