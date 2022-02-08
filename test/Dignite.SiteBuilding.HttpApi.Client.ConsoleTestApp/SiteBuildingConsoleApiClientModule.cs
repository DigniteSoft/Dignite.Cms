using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SiteBuildingHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class SiteBuildingConsoleApiClientModule : AbpModule
    {

    }
}
