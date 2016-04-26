using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CustomWise.Web.Startup))]
namespace CustomWise.Web {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
