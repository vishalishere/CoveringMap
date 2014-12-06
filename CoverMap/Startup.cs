using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoverMap.Startup))]
namespace CoverMap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
