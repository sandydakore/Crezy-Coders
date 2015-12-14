using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SDStudentPortal.Startup))]
namespace SDStudentPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
