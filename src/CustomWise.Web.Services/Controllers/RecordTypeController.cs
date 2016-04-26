using CustomWise.Data;
using DalEntities = CustomWise.Data.Entities;
using DtoEntities = CustomWise.Web.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

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
            return await _context.RecordTypes
                .Where(r => r.Id == id)
                .Select(r => new DtoEntities.RecordType {
                    Id = r.Id,
                    DisplayName = r.DisplayName,
                    SystemName = r.SystemName,
                    CreatedBy = r.CreatedBy,
                    CreatedDate = r.CreatedDate,
                    ModifiedBy = r.ModifiedBy,
                    ModifiedDate = r.ModifiedDate
                })
                .SingleAsync();
        }
    }
}
