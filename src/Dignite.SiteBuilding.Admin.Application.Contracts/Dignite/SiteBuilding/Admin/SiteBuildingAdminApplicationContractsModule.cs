using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding.Admin
{
    [DependsOn(
        typeof(SiteBuildingDomainSharedModule),
        typeof(SiteBuildingApplicationContractsSharedModule)
        )]
    public class SiteBuildingAdminApplicationContractsModule : AbpModule
    {

    }
}
