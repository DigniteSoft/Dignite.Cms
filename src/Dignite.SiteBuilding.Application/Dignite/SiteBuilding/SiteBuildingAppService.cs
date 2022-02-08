using Dignite.SiteBuilding.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding
{
    public abstract class SiteBuildingAppService : ApplicationService
    {
        protected SiteBuildingAppService()
        {
            LocalizationResource = typeof(SiteBuildingResource);
            ObjectMapperContext = typeof(SiteBuildingApplicationModule);
        }
    }
}
