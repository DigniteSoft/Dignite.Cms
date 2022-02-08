using Dignite.SiteBuilding.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(SiteBuildingEntityFrameworkCoreTestModule)
        )]
    public class SiteBuildingDomainTestModule : AbpModule
    {
        
    }
}
