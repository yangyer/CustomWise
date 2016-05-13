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

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/specification")]
    public class SpecificationController
        : BaseController {
        
        public SpecificationController()
            : base(new EfUnitOfWork(DataContextFactory.CreateDataContextFactory().GetContext()), AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperMapperInstance()) {
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
            var specification = await SpecificationRepository.FindAsync(id);
            return AutoMapper.Map<DtoEntities.Specification>(specification);
        }

        [Route]
        public async Task<int> Post(DtoEntities.Specification specification, bool published = false) {
            if(specification.ItemType.SystemName != "root") {
                throw new NotSupportedException($"Can only process root { nameof(DalEntities.Specification) } ");
            }

            var flatten = specification.Flatten(s => s.Subitems);
            var specs = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(flatten);
            var versionHeader = new DalEntities.VersionHeader();
            var latestVersionNumber = 1;
            var specVersions = new List<DalEntities.SpecificationVersion>();
            if (specification.ParentId > 0) {
                versionHeader = SpecificationVersionRepository.Get().Where(specVersion => specVersion.Id == specification.ParentId).First().VersionHeader;
                latestVersionNumber = versionHeader.SpecificationVersions.ToList().Select(ver => ver.VersionNumber.Split(',').Cast<int>().Max()).Max() + 1;

                // append latest version to those spec versions that do not exist in the list
                specVersions = versionHeader.SpecificationVersions.Where(sv => !specs.Any(s => s.Id == sv.Id))
                                                                  .Select(sv => {
                                                                      sv.VersionNumber += sv.VersionNumber.Length > 0 ? "," : "" + latestVersionNumber;
                                                                      sv.State = SophconEntityState.Modified;
                                                                      return sv;
                                                                  }).ToList();
            }

            // create new version for the modified and added specifications, omit the ones with deleted
            specVersions.AddRange(specs.Where(s => s.State.In(SophconEntityState.Added, SophconEntityState.Modified))
                                         .Select(s => {
                                             var specVersion = AutoMapper.Map<DalEntities.SpecificationVersion>(s);
                                             specVersion.VersionHeader = versionHeader;
                                             specVersion.VersionNumber = latestVersionNumber.ToString();
                                             specVersion.SpecificationId = s.Id;
                                             specVersion.State = SophconEntityState.Added;
                                             return specVersion;
                                         }));
            SpecificationRepository.AddRange(specs.Where(s => s.State == SophconEntityState.Added));
            SpecificationRepository.UpdateRange(specs.Where(s => s.State == SophconEntityState.Modified));
            SpecificationRepository.DeleteRange(specs.Where(s => s.State == SophconEntityState.Deleted));
            
            return await UnitOfWork.SaveChangesAsync();
        }

        [Route]
        public async Task<int> Put(DtoEntities.Specification specification) {
            if (specification.ItemType.SystemName != "root") {
                throw new NotSupportedException($"Can only process root { nameof(DalEntities.Specification) } ");
            }

            var flatten = specification.Flatten(s => s.Subitems);
            var specifications = AutoMapper.Map<IEnumerable<DalEntities.Specification>>(flatten);

            specifications = specifications.Select(s => {
                if (s.State == SophconEntityState.Deleted) {
                    s.Deleted = true;
                    s.State = SophconEntityState.Modified;
                }
                return s;
            });

            SpecificationRepository.AddRange(specifications.Where(s => s.State == SophconEntityState.Added));
            SpecificationRepository.UpdateRange(specifications.Where(s => s.State == SophconEntityState.Modified));
            
            return await UnitOfWork.SaveChangesAsync(
                preSave: (eleLs, state) => {
                    var specs = eleLs.Where(e => e is DalEntities.Specification).Select(e => e as DalEntities.Specification);
                    var versionHeader = SpecificationVersionRepository.Get().Where(specVersion => specVersion.Id == specification.Id).First().VersionHeader;
                    var latestVersionNumber = versionHeader.SpecificationVersions.ToList().Select(ver => ver.VersionNumber.Split(',').Select(vNum => int.Parse(vNum)).Max()).Max() + 1;

                    // append latest version to those spec versions that do not exist in the list
                    var specVersions = versionHeader.SpecificationVersions
                        .Where(sv => !specs.Any(s => s.Id == sv.SpecificationId)) // get versions of the specs that is not going to be updated/added in this pass
                        .Select(sv => new { SpecId = sv.SpecificationId, SpecVer = sv, VerNum = sv.VersionNumber.Split(',').Select(vNum => int.Parse(vNum)).Max() })
                        .GroupBy(svObj => svObj.SpecId) // group by the SpecificationId to get versions for that spec
                        .Select(svG => svG.OrderByDescending(svObj => svObj.VerNum).First().SpecVer) // order descending and take the first item (max version number)
                        .Select(sv => {
                            sv.VersionNumber = sv.VersionNumber + (sv.VersionNumber.Length > 0 ? "," : "") + latestVersionNumber;
                            sv.State = SophconEntityState.Modified;
                            return sv;
                        }).ToList();
                    
                    // create new version for the modified and added specifications, omit the ones with deleted
                    specVersions.AddRange(specs.Where(s => s.State.In(SophconEntityState.Added, SophconEntityState.Modified, SophconEntityState.Deleted))
                                                 .Select(s => {
                                                     var specVersion = AutoMapper.Map<DalEntities.SpecificationVersion>(s);
                                                     specVersion.VersionHeader = versionHeader;
                                                     specVersion.VersionNumber = latestVersionNumber.ToString();
                                                     specVersion.SpecificationId = s.Id;
                                                     specVersion.Action = s.State.ToString();
                                                     specVersion.State = SophconEntityState.Added;
                                                     return specVersion;
                                                 }));
                    
                    SpecificationVersionRepository.AddRange(specVersions.Where(sv => sv.State == SophconEntityState.Added));
                    SpecificationVersionRepository.UpdateRange(specVersions.Where(sv => sv.State == SophconEntityState.Modified));
                },
                postSave: (eleLs, state) => {
                    var x = eleLs;
                });
        }
    }
}
