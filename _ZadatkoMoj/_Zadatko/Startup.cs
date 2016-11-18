using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_Zadatko.Startup))]
namespace _Zadatko
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
