using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.Base;
using DtoEntities = CustomWise.Web.Services.Models;
using DalEntities = CustomWise.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Sophcon.Collections;

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/specification")]
    public class SpecificationController
        : BaseController {
        private ICustomWiseContext _context;
       
        public SpecificationController()
            : base(AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperMapperInstance()) {
            _context = new CustomWiseModel();
            _context.RegisterPreSave((entityList, state) => {
                entityList.ToList().ForEach(entity => {
                    entity.CreatedBy = entity.State == Sophcon.SophconEntityState.Added ? RequestContext.Principal.Identity.Name : entity.CreatedBy;
                    entity.CreatedDate = entity.State == Sophcon.SophconEntityState.Added ? DateTime.Now : entity.CreatedDate;
                    entity.ModifiedBy = RequestContext.Principal.Identity.Name;
                    entity.ModifiedDate = DateTime.Now;
                });
            });
        }

        public SpecificationController(ICustomWiseContext context, IConfigurationProvider autoMapperConfigProvier, IMapper mapper)
            : base(autoMapperConfigProvier, mapper) {
            _context = context;
        }

        [Route]
        public async Task<IEnumerable<DtoEntities.Specification>> Get() {
            return await _context.Specifications.ProjectToListAsync<DtoEntities.Specification>(AutoMapperConfigProvider);
        }

        [Route("{id:int}")]
        public async Task<DtoEntities.Specification> Get(int id) {
            return await _context.Specifications
                                 .Where(s => s.Id == id)
                                 .ProjectToSingleAsync<DtoEntities.Specification>(AutoMapperConfigProvider);
        }

//        [Route]
//        public async Task<int> Post(DtoEntities.Specification specification, bool published = false) {
//            var version = await GetOrCreateVersion(specification, published, true);
//            var newSpecification = AutoMapper.Map<DalEntities.Specification>(specification);
//            var newSubSpecifications = newSpecification.SubSpecifications.Flatten(s => s.SubSpecifications);

//            newSpecification.SpecificationVersionId = version.Id;
//            newSpecification.SpecificationVersion   = version;

//            newSubSpecifications.ToList().ForEach(s => {
//                s.SpecificationVersionId = version.Id;
//                s.SpecificationVersion   = version;
//            });

//            _context.Specifications.Add(newSpecification);
//            _context.Specifications.AddRange(newSubSpecifications);

//            var result = await _context.SaveChangesAsync();
//            // validate that the number of items presisted to the db is currect;
//            // 2 = 1 parent spec & 1 version
//            if(result != (newSubSpecifications.Count() + 2)) {
//#warning log or throw exception
//            }
//            return newSpecification.Id;
//        }

//        [Route]
//        public async Task<int> Put(DtoEntities.Specification specification, bool published = false) {
//            var currSpecification = await _context.Specifications.FindAsync(specification.Id);
//            if(currSpecification == null) {
//                return 0;
//            }
//            var version = await GetOrCreateVersion(specification, published);
//            if (version == null) {
//                return 0;
//            }
//            var currFlattenSpecs = currSpecification.SubSpecifications.Flatten(s => s.SubSpecifications);
//            var updatedFlattenSpecs = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(specification.SubSpecifications).Flatten(s => s.SubSpecifications);

//            updatedFlattenSpecs.ToList().ForEach(u => {
//                u.SpecificationVersionId = version.Id;
//                u.SpecificationVersion   = version;
//            });

//            // root
//            currSpecification.Order                  = specification.Order;
//            currSpecification.ParentSpecificationId  = specification.ParentSpecificationId;
//            currSpecification.DisplayName            = specification.DisplayName;
//            currSpecification.IsActive               = specification.IsActive;
//            currSpecification.Deleted                = specification.Deleted;
//            currSpecification.SpecificationTypeId    = specification.SpecificationTypeId;
//            currSpecification.SpecificationVersion   = version;
//            currSpecification.SpecificationVersionId = version.Id;
//            // sub specifications - update
//            currFlattenSpecs.Join(updatedFlattenSpecs, c => c.Id, u => u.Id,
//                (c, u) => {
//                    c.Order                  = u.Order;
//                    c.ParentSpecificationId  = u.ParentSpecificationId;
//                    c.DisplayName            = u.DisplayName;
//                    c.IsActive               = u.IsActive;
//                    c.Deleted                = u.Deleted;
//                    c.SpecificationTypeId    = u.SpecificationTypeId;
//                    c.SpecificationVersion   = u.SpecificationVersion;
//                    c.SpecificationVersionId = u.SpecificationVersionId;
//                    return c;
//                });
//            // sub specifications - delete
//            _context.Specifications.RemoveRange(currFlattenSpecs.Where(s => !s.Id.In(updatedFlattenSpecs.Select(u => u.Id))));
//            // sub specifications - create
//            _context.Specifications.AddRange(updatedFlattenSpecs.Where(s => !s.Id.In(currFlattenSpecs.Select(u => u.Id))));
//            var result = await _context.SaveChangesAsync();
//            // validate that the number of items presisted to the db is currect;
//            // 2 = 1 parent spec & 1 version
//            if (result != (updatedFlattenSpecs.Count() + 2)) {
//#warning log or throw exception
//            }
//            return currSpecification.Id;
//        }

//        internal async Task<DalEntities.SpecificationVersion> GetOrCreateVersion(DtoEntities.Specification specification, bool published, bool createIfNotFound = false) {
//            var version = await _context.SpecificationVersions.FindAsync(specification.SpecificationVersionId);
//            if(version == default(DalEntities.SpecificationVersion)) {
//                if(!createIfNotFound) {
//                    return version;
//                }

//                version = new DalEntities.SpecificationVersion { Name = specification.DisplayName };
//                _context.SpecificationVersions.Add(version);
//            }
//            version = version.Published ? new DalEntities.SpecificationVersion { Name = version.Name } : version;
//            version.Published = published;
//            version.PublishedDate = published ? DateTime.Now : (Nullable<DateTime>)null;
            
//            return version;
//        }
    }
}
