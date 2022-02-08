using Dignite.SiteBuilding.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dignite.SiteBuilding.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class SiteBuildingPageModel : AbpPageModel
    {
        protected SiteBuildingPageModel()
        {
            LocalizationResourceType = typeof(SiteBuildingResource);
            ObjectMapperContext = typeof(SiteBuildingWebModule);
        }
    }
}