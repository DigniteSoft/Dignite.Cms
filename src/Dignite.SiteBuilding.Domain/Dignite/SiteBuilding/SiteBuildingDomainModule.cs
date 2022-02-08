using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(SiteBuildingDomainSharedModule)
    )]
    public class SiteBuildingDomainModule : AbpModule
    {

    }
}
