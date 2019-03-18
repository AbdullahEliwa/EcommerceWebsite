using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceWebsite.Startup))]
namespace EcommerceWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
