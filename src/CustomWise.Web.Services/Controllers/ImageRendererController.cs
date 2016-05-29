using AutoMapper;
using CustomWise.Web.Services.Controllers.Base;
using Sophcon.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using CustomWise.Imaging;

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

        [Route]
        public HttpResponseMessage Get ()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();

            using (var img = ImageUtilities.createSolidColorImage(100, 100, System.Drawing.Color.Red)) { 
                img.Save(stream, ImageFormat.Png);
                response.Content = new ByteArrayContent(stream.ToArray());
            }

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return response;
        }
    }
}
