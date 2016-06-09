using Sophcon;
using Sophcon.Collections;
using Sophcon.Data;
using Sophcon.Data.Infrastructure;
using System;
using System.Collections.Generic;

namespace CustomWise.Web.Services.Controllers.SaveEvents {
    public class SetCreatedModifiedDataPreSaveEvent<TUnitOfWork> : SaveStrategyBase<TUnitOfWork> 
        where TUnitOfWork : IUnitOfWork {

        private string _userName;

        public SetCreatedModifiedDataPreSaveEvent(string userName) {
            _userName = !string.IsNullOrWhiteSpace(userName) ? userName : "system";
        }

        protected override void _ExecutePreSave(IEnumerable<DataEntityWrapper> entries) {
            foreach (var entry in entries) {
                entry.Entity.CreatedBy = entry.State == DataEntityState.Added ? _userName : entry.Entity.CreatedBy;
                entry.Entity.CreatedDate = entry.State == DataEntityState.Added ? DateTime.Now : entry.Entity.CreatedDate;
                entry.Entity.ModifiedBy = entry.State.In(DataEntityState.Modified, DataEntityState.Added) ? _userName : entry.Entity.ModifiedBy;
                entry.Entity.ModifiedDate = entry.State.In(DataEntityState.Modified, DataEntityState.Added) ? DateTime.Now : entry.Entity.ModifiedDate;
            }
        }

        protected override void _PreSaveError(SaveException thrownException) {
            Token.SetExitAll();
            Token.SetException(thrownException);
        }
    }
}
