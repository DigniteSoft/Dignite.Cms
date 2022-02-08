using Dignite.SiteBuilding.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Dignite.SiteBuilding.Admin.Blazor.Server.Host
{
    public abstract class SiteBuildingComponentBase : AbpComponentBase
    {
        protected SiteBuildingComponentBase()
        {
            LocalizationResource = typeof(SiteBuildingResource);
        }
    }
}
