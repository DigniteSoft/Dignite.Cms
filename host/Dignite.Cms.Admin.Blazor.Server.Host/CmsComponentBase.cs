using Dignite.Cms.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Dignite.Cms.Admin.Blazor.Server.Host
{
    public abstract class CmsComponentBase : AbpComponentBase
    {
        protected CmsComponentBase()
        {
            LocalizationResource = typeof(CmsResource);
        }
    }
}
