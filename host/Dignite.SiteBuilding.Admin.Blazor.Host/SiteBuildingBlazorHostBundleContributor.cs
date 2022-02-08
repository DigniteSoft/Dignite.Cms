using Volo.Abp.Bundling;

namespace Dignite.SiteBuilding.Admin.Blazor.Host
{
    public class SiteBuildingBlazorHostBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {

        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}
