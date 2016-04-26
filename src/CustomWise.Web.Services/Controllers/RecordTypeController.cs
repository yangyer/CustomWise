using CustomWise.Data;
using DalEntities = CustomWise.Data.Entities;
using DtoEntities = CustomWise.Web.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace CustomWise.Web.Services.Controllers {
    public class RecordTypeController 
        : ApiController {
        private CustomWiseModel _context;

        public RecordTypeController() {
            _context = new CustomWiseModel();
        }

        public RecordTypeController(CustomWiseModel context) {
            _context = context;
        }

        public async Task<IEnumerable<DtoEntities.RecordType>> Get() {
            return await _context.RecordTypes
                .Select(r => new DtoEntities.RecordType {
                    Id = r.Id,
                    DisplayName = r.DisplayName,
                    SystemName = r.SystemName,
                    CreatedBy = r.CreatedBy,
                    CreatedDate = r.CreatedDate,
                    ModifiedBy = r.ModifiedBy,
                    ModifiedDate = r.ModifiedDate
                })
                .ToArrayAsync();
        }

        public async Task<DtoEntities.RecordType> Get(int id) {
            return (await Get())
                .Where(r => r.Id == id)
                .Single();
        }

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
