using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProject_YejunSon.Startup))]
namespace PassionProject_YejunSon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
