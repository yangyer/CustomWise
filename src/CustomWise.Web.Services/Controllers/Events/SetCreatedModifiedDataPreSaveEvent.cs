﻿using Sophcon;
using Sophcon.Collections;
using Sophcon.Data;
using System;
using System.Collections.Generic;

namespace CustomWise.Web.Services.Controllers.Events {
    public class SetCreatedModifiedDataPreSaveEvent
        : PreSaveEventBase {

        private string _userName;

        public SetCreatedModifiedDataPreSaveEvent(string userName) 
            : base(typeof(BaseEntity)) {
            _userName = !string.IsNullOrWhiteSpace(userName) ? userName : "system";
        }

        public override void PreSaveAction(IEnumerable<BaseEntity> entities, IPreSavePipeline state) {
            foreach(var entity in entities) {
                entity.CreatedBy = entity.State == SophconEntityState.Added ? _userName : entity.CreatedBy;
                entity.CreatedDate = entity.State == SophconEntityState.Added ? DateTime.Now : entity.CreatedDate;
                entity.ModifiedBy = entity.State.In(SophconEntityState.Modified, SophconEntityState.Added) ? _userName : entity.ModifiedBy;
                entity.ModifiedDate = entity.State.In(SophconEntityState.Modified, SophconEntityState.Added) ? DateTime.Now : entity.ModifiedDate;
            }
        }
    }
}
