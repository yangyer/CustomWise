using AutoMapper;
using CustomWise.Web.Services.Controllers.Base;
using Sophcon;
using Sophcon.Collections;
using Sophcon.Data.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using DalEntities = CustomWise.Data.Entities;
using DtoEntities = CustomWise.Web.Services.Models;

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/specification")]
    public class SpecificationController
        : BaseController {

        public SpecificationController()
            : base(DataContextFactory.CreateDataContextFactory().GetContext(), AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperMapperInstance()) {
        }

        public SpecificationController(IDbContext context, IConfigurationProvider autoMapperConfigProvier, IMapper mapper)
            : base(context, autoMapperConfigProvier, mapper) {
        }

        [Route]
        public async Task<IEnumerable<DtoEntities.Specification>> Get() {
            var specifications = UnitOfWork.GetRepository<DalEntities.Specification>().List().ToList();
            var specs = AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications);
            return specs;
        }

        [Route("{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id) {
            var specification = UnitOfWork.GetRepository<DalEntities.Specification>().Find(id);
            var spec = AutoMapper.Map<DtoEntities.Specification>(specification);
            return spec;
        }

        [Route("{version:int}/{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id, int version) {
            var specification = await UnitOfWork.GetRepository<DtoEntities.Specification>().FindAsync(id);
            return AutoMapper.Map<DtoEntities.Specification>(specification);
        }

        [Route("{typeName}")]
        public async Task<IEnumerable<DtoEntities.Specification>> Get(string typeName) {
            var specifications = UnitOfWork.GetRepository<DalEntities.Specification>().List().Where(s => s.SpecificationSystemType.Name.Equals(typeName, System.StringComparison.OrdinalIgnoreCase)).ToList();
            return AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications);
        }

        [Route("parent/{id:int}")]
        public async Task<IEnumerable<DtoEntities.Specification>> GetByParentID(int id) {
            var specifications = await UnitOfWork.GetRepository<DalEntities.Specification>().FindAsync(id);
            return AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications);
        }

        [Route]
        public async Task<int> Post(DtoEntities.Specification specification, bool published = false) {
            //var versionUow = new VersionUnitOfWorkWrapper(UnitOfWork as UnitOfWorkBase<IDbContext>)
            //    .Register<DalEntities.Specification, DalEntities.SpecificationVersion>(new SpecificationVersionRegistration(SpecificationVersionRepository))
            //    .Register<DalEntities.MetaData, DalEntities.MetaDataVersion>(new MetaDataVersionRegistration(MetaDataVersionRepository));
            var specifications = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(specification).Flatten(s => s.SubItems);

            specifications = specifications.Select(s => {
                if (s.State == DataEntityState.Deleted) {
                    s.Deleted = true;
                    s.State = DataEntityState.Modified;
                }
                return s;
            });

            UnitOfWork.GetRepository<DalEntities.Specification>().AddRange(specifications.Where(s => s.State == DataEntityState.Added));
            UnitOfWork.GetRepository<DalEntities.Specification>().UpdateRange(specifications.Where(s => s.State == DataEntityState.Modified));

            return await UnitOfWork.CommitChangesAsync(new CancellationTokenSource().Token);
        }

        [Route]
        public async Task<int> Put(DtoEntities.Specification specification) {
            //var versionUow = new VersionUnitOfWorkWrapper(UnitOfWork as UnitOfWorkBase<IDbContext>)
            //    .Register<DalEntities.Specification, DalEntities.SpecificationVersion>(new SpecificationVersionRegistration(SpecificationVersionRepository))
            //    .Register<DalEntities.MetaData, DalEntities.MetaDataVersion>(new MetaDataVersionRegistration(MetaDataVersionRepository));

            var specifications = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(specification).Flatten(s => s.SubItems);
            
            specifications = specifications.Select(s => {
                if (s.State == DataEntityState.Deleted) {
                    s.Deleted = true;
                    s.State = DataEntityState.Modified;
                }
                return s;
            });

            UnitOfWork.GetRepository<DalEntities.Specification>().AddRange(specifications.Where(s => s.State == DataEntityState.Added));
            UnitOfWork.GetRepository<DalEntities.Specification>().UpdateRange(specifications.Where(s => s.State == DataEntityState.Modified));
            
            return await UnitOfWork.CommitChangesAsync(new CancellationTokenSource().Token);
        }
    }
}
