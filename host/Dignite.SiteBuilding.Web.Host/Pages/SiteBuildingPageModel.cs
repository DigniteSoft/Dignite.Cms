using Dignite.SiteBuilding.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dignite.SiteBuilding.Pages
{
    public abstract class SiteBuildingPageModel : AbpPageModel
    {
        protected SiteBuildingPageModel()
        {
            LocalizationResourceType = typeof(SiteBuildingResource);
        }
    }
}