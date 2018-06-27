using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoOpHub.Startup))]
namespace CoOpHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
