using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;

namespace Cosmos.SqlApiDemo
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("ProductList", "", "Views/ProductList.dothtml");
            config.RouteTable.Add("ProductDetail", "product/{Id?}", "Views/ProductDetail.dothtml");
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            ((ScriptResource)config.Resources.FindResource("jquery")).Location = new UrlResourceLocation("/lib/jquery/dist/jquery.min.js");

            config.Resources.Register("bootstrap-css", new StylesheetResource()
            {
                Location = new UrlResourceLocation("/lib/bootstrap/dist/css/bootstrap.min.css")
            });

            config.Resources.Register("bootstrap", new ScriptResource()
            {
                Location = new UrlResourceLocation("/lib/bootstrap/dist/js/bootstrap.min.js"),
                Dependencies = new [] { "bootstrap-css", "jquery" }
            });
        }
    }
}
