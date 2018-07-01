using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoListProject.Startup))]
namespace TodoListProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
