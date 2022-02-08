using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Dignite.SiteBuilding
{
    [Dependency(ReplaceServices = true)]
    public class SiteBuildingBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SiteBuilding";
    }
}
