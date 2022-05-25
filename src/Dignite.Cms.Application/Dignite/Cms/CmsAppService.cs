using Dignite.Cms.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Cms
{
    public abstract class CmsAppService : ApplicationService
    {
        protected CmsAppService()
        {
            LocalizationResource = typeof(CmsResource);
            ObjectMapperContext = typeof(CmsApplicationModule);
        }
    }
}
