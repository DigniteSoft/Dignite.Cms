using Microsoft.Extensions.DependencyInjection;
using Dignite.SiteBuilding.Admin.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Dignite.Abp.AntDesignBlazorUI;
using Volo.Abp.Threading;
using Volo.Abp.ObjectExtending.Modularity;
using Dignite.SiteBuilding.Admin.Pages;
using Dignite.Abp.ObjectExtending;
using Dignite.Abp.FieldCustomizing.Blazor;
using Dignite.Abp.BlazoriseUI;

namespace Dignite.SiteBuilding.Admin.Blazor
{
    [DependsOn(
        typeof(SiteBuildingAdminApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule),
        typeof(DigniteAbpAntDesignBlazorUIModule),
        typeof(DigniteAbpFieldCustomizingBlazorComponentsModule),
        typeof(DigniteAbpBlazoriseUIModule)
        )]
    public class SiteBuildingAdminBlazorModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<SiteBuildingAdminBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<SiteBuildingAdminBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new SiteBuildingAdminMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(SiteBuildingAdminBlazorModule).Assembly);
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        SiteBuildingModuleExtensionConsts.ModuleName,
                        SiteBuildingModuleExtensionConsts.EntityNames.Page,
                        createFormTypes: new[] { typeof(PageCreateDto) },
                        editFormTypes: new[] { typeof(PageUpdateDto) }
                    );

            });
        }
    }
}