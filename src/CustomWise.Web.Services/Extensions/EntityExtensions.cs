using Sophcon.Data.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWise.Web.Services.Extensions {
    public static class EntityExtensions {
        public static int MaxVersionNumber(this IVersionable versionableItem) {
            return versionableItem.VersionNumber.Split(',').Select(vNum => int.Parse(vNum)).Max();
        }
    }
}
