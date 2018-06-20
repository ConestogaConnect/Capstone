using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConestogaConnect.Startup))]
namespace ConestogaConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
