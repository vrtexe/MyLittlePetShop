using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyLittlePetShop.Startup))]
namespace MyLittlePetShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
