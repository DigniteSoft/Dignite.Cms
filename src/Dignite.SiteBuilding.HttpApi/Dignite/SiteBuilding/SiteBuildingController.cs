using Dignite.SiteBuilding.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.SiteBuilding
{
    public abstract class SiteBuildingController : AbpControllerBase
    {
        protected SiteBuildingController()
        {
            LocalizationResource = typeof(SiteBuildingResource);
        }
    }
}
