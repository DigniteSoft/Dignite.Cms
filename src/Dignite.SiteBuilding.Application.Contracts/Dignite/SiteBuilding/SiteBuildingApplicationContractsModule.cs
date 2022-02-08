using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(SiteBuildingApplicationContractsSharedModule)
        )]
    public class SiteBuildingApplicationContractsModule : AbpModule
    {

    }
}
