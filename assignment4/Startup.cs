using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(assignment4.Startup))]
namespace assignment4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
