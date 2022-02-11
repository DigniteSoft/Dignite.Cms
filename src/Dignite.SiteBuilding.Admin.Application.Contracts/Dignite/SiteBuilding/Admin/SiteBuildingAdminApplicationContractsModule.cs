using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding.Admin
{
    [DependsOn(
        typeof(SiteBuildingApplicationContractsSharedModule)
        )]
    public class SiteBuildingAdminApplicationContractsModule : AbpModule
    {

    }
}
