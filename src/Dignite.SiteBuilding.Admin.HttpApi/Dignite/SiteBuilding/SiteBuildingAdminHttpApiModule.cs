using Localization.Resources.AbpUi;
using Dignite.SiteBuilding.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.SiteBuilding.Admin
{
    [DependsOn(
        typeof(SiteBuildingAdminApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class SiteBuildingAdminHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(SiteBuildingAdminHttpApiModule).Assembly);
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
