using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminNG.Startup))]
namespace AdminNG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
