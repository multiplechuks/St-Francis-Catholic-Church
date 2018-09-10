using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StFrancisChurch.Startup))]
namespace StFrancisChurch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
