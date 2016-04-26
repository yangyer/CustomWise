using CustomWise.Data;
using DalEntities = CustomWise.Data.Entities;
using DtoEntities = CustomWise.Web.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System;

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

        public async Task Put(DtoEntities.RecordType recordType) {
            //var recordTypeToUpdate = await Get(recordType.Id);
            //if(recordTypeToUpdate == null) {
            //    throw new NullReferenceException("The RecordTykpe does not exist in the system.");
            //}
            throw new NotImplementedException();
        }
    }
}
