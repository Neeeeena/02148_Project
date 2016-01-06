using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_02148_Project.Website.Startup))]
namespace _02148_Project.Website
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
