using Localization.Resources.AbpUi;
using Dignite.SiteBuilding.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(SiteBuildingApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class SiteBuildingHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(SiteBuildingHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<SiteBuildingResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
