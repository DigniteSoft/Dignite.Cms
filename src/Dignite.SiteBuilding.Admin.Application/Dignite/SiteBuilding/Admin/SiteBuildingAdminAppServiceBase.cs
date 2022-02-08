using Dignite.SiteBuilding.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding.Admin
{
    public abstract class SiteBuildingAdminAppServiceBase : ApplicationService
    {
        protected SiteBuildingAdminAppServiceBase()
        {
            LocalizationResource = typeof(SiteBuildingResource);
            ObjectMapperContext = typeof(SiteBuildingAdminApplicationModule);
        }
    }
}
