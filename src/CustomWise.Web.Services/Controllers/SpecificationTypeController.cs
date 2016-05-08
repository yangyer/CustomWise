using AutoMapper;
using CustomWise.Data;
using CustomWise.Web.Services.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using DalEntities = CustomWise.Data.Entities;
using DtoEntities = CustomWise.Web.Services.Models;

namespace CustomWise.Web.Services.Controllers {
    [RoutePrefix("api/customwise/specificationType")]
    public class SpecificationTypeController 
        : BaseController {
        private ICustomWiseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationTypeController"/> class.
        /// </summary>
        public SpecificationTypeController()
            : base(new CustomWiseModel(), AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperMapperInstance()) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationTypeController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configProvider">The configuration provider.</param>
        public SpecificationTypeController(ICustomWiseContext context, IConfigurationProvider autoMapperConfigProvier, IMapper mapper)
            : base(context, autoMapperConfigProvier, mapper) {
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{T}"/>.</returns>
        [Route]
        public async Task<IEnumerable<DtoEntities.SpecificationType>> Get() {
            return await _context.SpecificationTypes
                .ProjectToListAsync<DtoEntities.SpecificationType>(AutoMapperConfigProvider);
        }

        /// <summary>
        /// Get a single <see cref="DtoEntities.SpecificationType"/> for specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns <see cref="DtoEntities.SpecificationType"/></returns>
        [Route("{id:int}")]
        public async Task<DtoEntities.SpecificationType> Get(int id) {
            return await _context.SpecificationTypes
                                       .Where(r => r.Id == id)
                                       .ProjectToSingleAsync<DtoEntities.SpecificationType>(AutoMapperConfigProvider);
        }

        /// <summary>
        /// Posts the specified <see cref="DtoEntities.SpecificationType" />.
        /// </summary>
        /// <param name="specificationType">The <see cref="DtoEntities.SpecificationType" /></param>
        /// <returns>
        /// Returns the new identifier of the <see cref="DtoEntities.SpecificationType" />
        /// </returns>
        /// <exception cref="System.NullReferenceException"></exception>
        [Route]
        public async Task<int> Post(DtoEntities.SpecificationType specificationType) {
            if (await _context.SpecificationTypes.AnyAsync(r => r.Id == specificationType.Id)) {
                throw new NullReferenceException($"The {nameof(specificationType)} already exist in the system.");
            }

            try {
                var recordTypeToAdd = new DalEntities.SpecificationType {
                    DisplayName = specificationType.DisplayName,
                    SystemName = specificationType.SystemName
                };
                _context.SpecificationTypes.Add(recordTypeToAdd);
                var result = await _context.SaveChangesAsync();

                return recordTypeToAdd.Id;
            } catch (DbUpdateConcurrencyException dbUpdateConcurrencyExc) {
                throw;
            } catch (DbUpdateException dbUpdateExc) {
                throw;
            } catch (DbEntityValidationException dbEntityValidationEx) {
                throw;
            } catch (NotSupportedException notSupportedEx) {
                throw;
            } catch (ObjectDisposedException objectDisposiedEx) {
                throw;
            } catch (InvalidOperationException invalidOpperationEx) {
                throw;
            }
        }

        /// <summary>
        /// Update the specified <see cref="DtoEntities.SpecificationType"/>.
        /// </summary>
        /// <param name="specificationType">Type of the specification type.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        [Route]
        public async Task Put(DtoEntities.SpecificationType specificationType) {
            var recordTypeToUpdate = await _context.SpecificationTypes.SingleOrDefaultAsync(r => r.Id == specificationType.Id);

            if (recordTypeToUpdate == null) {
                throw new NullReferenceException($"The {nameof(specificationType)} does not exist in the system.");
            }
            try {
                recordTypeToUpdate.DisplayName = specificationType.DisplayName;
                recordTypeToUpdate.SystemName = specificationType.SystemName;

                var result = await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException dbUpdateConcurrencyExc) {
                throw;
            } catch (DbUpdateException dbUpdateExc) {
                throw;
            } catch (DbEntityValidationException dbEntityValidationEx) {
                throw;
            } catch (NotSupportedException notSupportedEx) {
                throw;
            } catch (ObjectDisposedException objectDisposiedEx) {
                throw;
            } catch (InvalidOperationException invalidOpperationEx) {
                throw;
            }
        }
    }
}
