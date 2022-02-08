using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(SiteBuildingApplicationModule),
        typeof(SiteBuildingDomainTestModule)
        )]
    public class SiteBuildingApplicationTestModule : AbpModule
    {

    }
}
