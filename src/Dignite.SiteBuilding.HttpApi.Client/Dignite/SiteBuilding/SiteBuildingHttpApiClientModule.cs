using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(SiteBuildingApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class SiteBuildingHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SiteBuildingApplicationContractsModule).Assembly,
                SiteBuildingRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SiteBuildingHttpApiClientModule>();
            });

        }
    }
}
