using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarkTheWorld.Startup))]
namespace MarkTheWorld
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
