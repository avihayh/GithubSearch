using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Isracard.Startup))]
namespace Isracard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
