using AutoMapper;
using CustomWise.Web.Services.Controllers.Base;
using Sophcon;
using Sophcon.Collections;
using Sophcon.Data;
using Sophcon.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DalEntities = CustomWise.Data.Entities;
using DtoEntities = CustomWise.Web.Services.Models;
using CustomWise.Web.Services.Extensions;

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
            using (var ctx = new CustomWise.Data.CustomWiseModel()) {
                var specifications = await
                    (from s in ctx.Specifications
                     join a in ctx.Artifacts on s.ArtifactReferenceId equals a.Id.ToString() into g
                     from a in g.DefaultIfEmpty()
                     select new DtoEntities.ArtifactSpecification { Specification = s, Artifact = a }).ToListAsync();

                var specs = AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications.Where(obj => obj.Artifact == null).Select(obj => obj.Specification)).ToList();
                specs.AddRange(AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications.Where(obj => obj.Artifact != null).Select(obj => obj.Artifact)));

                return specs;
            }
        }

        [Route("{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id) {
            using (var ctx = new CustomWise.Data.CustomWiseModel()) {
                var obj = await
                    (from s in ctx.Specifications
                     join a in ctx.Artifacts on s.ArtifactReferenceId equals a.Id.ToString() into g
                     from a in g.DefaultIfEmpty()
                     where s.Id == id
                     select new DtoEntities.ArtifactSpecification { Specification = s, Artifact = a }).FirstOrDefaultAsync();

                return obj.Artifact == null ? AutoMapper.Map<DtoEntities.Specification>(obj.Specification) : AutoMapper.Map<DtoEntities.Specification>(obj.Artifact);
            }
        }

        [Route("{version:int}/{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id, int version) {
            var specification = await SpecificationRepository.FindAsync(id);
            return AutoMapper.Map<DtoEntities.Specification>(specification);
        }

        [Route("{typeName}")]
        public async Task<IEnumerable<DtoEntities.Specification>> Get(string typeName) {
            var specifications = await SpecificationRepository.Get().Where(s => s.ItemType.SystemName == typeName).ToListAsync();
            return AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications);
        }

        [Route("parent/{id:int}")]
        public async Task<IEnumerable<DtoEntities.Specification>> GetByParentId(int id) {
            using (var ctx = new CustomWise.Data.CustomWiseModel()) {
                var specifications = await
                    (from s in ctx.Specifications
                     join a in ctx.Artifacts on s.ArtifactReferenceId equals a.Id.ToString() into g
                     from a in g.DefaultIfEmpty()
                     where s.ParentId == id 
                     select new DtoEntities.ArtifactSpecification { Specification = s, Artifact = a }).ToListAsync();

                var specs = AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications.Where(obj => obj.Artifact == null).Select(obj => obj.Specification)).ToList();
                specs.AddRange(AutoMapper.Map<IEnumerable<DtoEntities.Specification>>(specifications.Where(obj => obj.Artifact != null).Select(obj => obj.Artifact)));

                return specs;
            }
        }

        [Route]
        public async Task<int> Post(DtoEntities.Specification specification, bool published = false) {
            //var versionUow = new VersionUnitOfWorkWrapper(UnitOfWork as UnitOfWorkBase<IDbContext>)
            //    .Register<DalEntities.Specification, DalEntities.SpecificationVersion>(new SpecificationVersionRegistration(SpecificationVersionRepository))
            //    .Register<DalEntities.MetaData, DalEntities.MetaDataVersion>(new MetaDataVersionRegistration(MetaDataVersionRepository));
            var specifications = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(specification).Flatten(s => s.SubItems);

            specifications = specifications.Select(s => {
                if (s.State == SophconEntityState.Deleted) {
                    s.Deleted = true;
                    s.State = SophconEntityState.Modified;
                }
                return s;
            });

            SpecificationRepository.AddRange(specifications.Where(s => s.State == SophconEntityState.Added));
            SpecificationRepository.UpdateRange(specifications.Where(s => s.State == SophconEntityState.Modified));

            return await UnitOfWork.SaveChangesAsync();
        }

        [Route]
        public async Task<int> Put(DtoEntities.Specification specification) {
            //var versionUow = new VersionUnitOfWorkWrapper(UnitOfWork as UnitOfWorkBase<IDbContext>)
            //    .Register<DalEntities.Specification, DalEntities.SpecificationVersion>(new SpecificationVersionRegistration(SpecificationVersionRepository))
            //    .Register<DalEntities.MetaData, DalEntities.MetaDataVersion>(new MetaDataVersionRegistration(MetaDataVersionRepository));

            var specifications = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(specification).Flatten(s => s.SubItems);
            
            specifications = specifications.Select(s => {
                if (s.State == SophconEntityState.Deleted) {
                    s.Deleted = true;
                    s.State = SophconEntityState.Modified;
                }
                return s;
            });

            SpecificationRepository.AddRange(specifications.Where(s => s.State == SophconEntityState.Added));
            SpecificationRepository.UpdateRange(specifications.Where(s => s.State == SophconEntityState.Modified));
            
            return await UnitOfWork.SaveChangesAsync();
        }
    }
}
