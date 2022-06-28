using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(neveinNadeen.Startup))]
namespace neveinNadeen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
