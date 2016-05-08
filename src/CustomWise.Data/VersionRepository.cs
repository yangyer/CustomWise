using CustomWise.Data.Entities.Versioning;
using Sophcon;
using Sophcon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWise.Data {
    public class VersionRepository<T>
        : RepositoryBase<T>
        where T : BaseEntity, IEntityVersion, new() {

        public override void Add(T entity) {
            throw new NotImplementedException();
        }

        public override void AddRange(IEnumerable<T> entities) {
            throw new NotImplementedException();
        }

        public override void Delete(T entity) {
            throw new NotImplementedException();
        }

        public override void DeleteRange(IEnumerable<T> entities) {
            throw new NotImplementedException();
        }

        public override T Find(params object[] keys) {
            throw new NotImplementedException();
        }

        public override Task<T> FindAsync(params object[] keys) {
            throw new NotImplementedException();
        }

        public override IQueryable<T> Get() {
            throw new NotImplementedException();
        }

        public override void Update(T entity) {
            throw new NotImplementedException();
        }

        public override void UpdateRange(IEnumerable<T> entities) {
            throw new NotImplementedException();
        }
    }
}
