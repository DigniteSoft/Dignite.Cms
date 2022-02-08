using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Dignite.SiteBuilding.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Dignite.Abp.FieldCustomizing;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(DigniteAbpFieldCustomizingModule)
    )]
    public class SiteBuildingDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SiteBuildingDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<SiteBuildingResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Dignite/SiteBuilding/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("SiteBuilding", typeof(SiteBuildingResource));
            });
        }
    }
}
