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
    [RoutePrefix("api/customwise/recordtype")]
    public class RecordTypeController 
        : BaseController {
        private CustomWiseModel _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordTypeController"/> class.
        /// </summary>
        public RecordTypeController()
            : base(AutoMapperFactory.CreateAutoMapperConfigProviderInstance()) {
            _context = new CustomWiseModel();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordTypeController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configProvider">The configuration provider.</param>
        public RecordTypeController(CustomWiseModel context, IConfigurationProvider autoMapperConfigProvier)
            : base(autoMapperConfigProvier) {
            _context = context;
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{T}"/>.</returns>
        [Route]
        public async Task<IEnumerable<DtoEntities.RecordType>> Get() {
            return await _context.RecordTypes
                .ProjectToListAsync<DtoEntities.RecordType>(AutoMapperConfigProvier);
        }

        /// <summary>
        /// Get a single <see cref="DtoEntities.RecordType"/> for specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns <see cref="DtoEntities.RecordType"/></returns>
        [Route("{id:int}")]
        public async Task<DtoEntities.RecordType> Get(int id) {
            var result = await _context.RecordTypes
                                       .Where(r => r.Id == id)
                                       .ProjectToSingleAsync<DtoEntities.RecordType>(AutoMapperConfigProvier);

            //if(result == default(DtoEntities.RecordType)) {
            //    return NotFound();
            //}

            return result;
        }

        /// <summary>
        /// Posts the specified record type.
        /// </summary>
        /// <param name="recordType">The <see cref="DtoEntities.RecordType"/></param>
        /// <returns>Returns the new identifier of the <see cref="DtoEntities.RecordType"/></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        [Route]
        public async Task<int> Post(DtoEntities.RecordType recordType) {
            if (await _context.RecordTypes.AnyAsync(r => r.Id == recordType.Id)) {
                throw new NullReferenceException($"The {nameof(recordType)} already exist in the system.");
            }

            try {
                var recordTypeToAdd = new DalEntities.RecordType {
                    DisplayName = recordType.DisplayName,
                    SystemName = recordType.SystemName
                };
                _context.RecordTypes.Add(recordTypeToAdd);
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
        /// Puts the specified record type.
        /// </summary>
        /// <param name="recordType">Type of the record.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        [Route]
        public async Task Put(DtoEntities.RecordType recordType) {
            var recordTypeToUpdate = await _context.RecordTypes.SingleOrDefaultAsync(r => r.Id == recordType.Id);

            if (recordTypeToUpdate == null) {
                throw new NullReferenceException($"The {nameof(recordType)} does not exist in the system.");
            }
            try {
                recordTypeToUpdate.DisplayName = recordType.DisplayName;
                recordTypeToUpdate.SystemName = recordType.SystemName;

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
