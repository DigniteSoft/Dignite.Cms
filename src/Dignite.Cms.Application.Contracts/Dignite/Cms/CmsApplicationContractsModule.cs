using Volo.Abp.Modularity;

namespace Dignite.Cms
{
    [DependsOn(
        typeof(CmsApplicationContractsSharedModule)
        )]
    public class CmsApplicationContractsModule : AbpModule
    {

    }
}
