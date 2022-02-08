using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(SiteBuildingDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class SiteBuildingApplicationContractsSharedModule : AbpModule
    {

    }
}
