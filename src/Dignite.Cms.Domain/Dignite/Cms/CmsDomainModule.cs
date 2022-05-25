using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Cms
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(CmsDomainSharedModule)
    )]
    public class CmsDomainModule : AbpModule
    {

    }
}
