using Dignite.Cms.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Cms
{
    public abstract class CmsController : AbpControllerBase
    {
        protected CmsController()
        {
            LocalizationResource = typeof(CmsResource);
        }
    }
}
