using Volo.Abp.Modularity;

namespace Dignite.Cms.Admin
{
    [DependsOn(
        typeof(CmsApplicationContractsSharedModule)
        )]
    public class CmsAdminApplicationContractsModule : AbpModule
    {

    }
}
