using Dignite.SiteBuilding.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.SiteBuilding.Admin
{
    public abstract class SiteBuildingAdminController : AbpControllerBase
    {
        protected SiteBuildingAdminController()
        {
            LocalizationResource = typeof(SiteBuildingResource);
        }
    }
}
