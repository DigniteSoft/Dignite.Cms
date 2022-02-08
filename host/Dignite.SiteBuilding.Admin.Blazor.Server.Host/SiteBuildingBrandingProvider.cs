using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.SiteBuilding.Admin.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class SiteBuildingBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SiteBuilding";
    }
}
