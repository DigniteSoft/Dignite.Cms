using Localization.Resources.AbpUi;
using Dignite.Cms.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.FileManagement;

namespace Dignite.Cms
{
    [DependsOn(
        typeof(CmsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(FileManagementHttpApiModule)
        )]
    public class CmsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<CmsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
