using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataDoctor.Startup))]
namespace DataDoctor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
