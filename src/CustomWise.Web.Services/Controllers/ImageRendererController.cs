using AutoMapper;
using CustomWise.Web.Services.Controllers.Base;
using Sophcon.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomWise.Web.Services.Controllers
{
    [RoutePrefix("api/customwise/configuration/renderer")]
    public class ImageRendererController : BaseController
    {
        public ImageRendererController()
            : base(DataContextFactory.CreateDataContextFactory().GetContext(), AutoMapperFactory.CreateAutoMapperConfigProviderInstance(), AutoMapperFactory.CreateAutoMapperMapperInstance())
        { }

        public ImageRendererController(IDbContext context, IConfigurationProvider autoMapperConfigProvier, IMapper mapper)
            : base (context, autoMapperConfigProvier, mapper)
        { }
    }
}
