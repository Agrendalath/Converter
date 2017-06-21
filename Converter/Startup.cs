using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Converter.Startup))]
namespace Converter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
