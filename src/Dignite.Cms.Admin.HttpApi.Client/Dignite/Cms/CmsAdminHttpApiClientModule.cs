using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Cms.Admin
{
    [DependsOn(
        typeof(CmsAdminApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class CmsAdminHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(CmsAdminApplicationContractsModule).Assembly,
                CmsAdminRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CmsAdminHttpApiClientModule>();
            });

        }
    }
}
