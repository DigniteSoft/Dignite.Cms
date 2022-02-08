using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.SiteBuilding.Admin
{
    [DependsOn(
        typeof(SiteBuildingAdminApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class SiteBuildingAdminHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SiteBuildingAdminApplicationContractsModule).Assembly,
                SiteBuildingAdminRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SiteBuildingAdminHttpApiClientModule>();
            });

        }
    }
}
