using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Cms.Admin.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class CmsBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Cms";
    }
}
