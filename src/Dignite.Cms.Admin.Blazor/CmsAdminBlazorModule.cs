using Microsoft.Extensions.DependencyInjection;
using Dignite.Cms.Admin.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Threading;
using Volo.Abp.ObjectExtending.Modularity;
using Dignite.Cms.Admin.Pages;
using Dignite.Abp.ObjectExtending;
using Dignite.Abp.FieldCustomizing.Blazor;
using Dignite.Abp.BlazoriseUI;

namespace Dignite.Cms.Admin.Blazor
{
    [DependsOn(
        typeof(CmsAdminApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule),
        typeof(DigniteAbpFieldCustomizingBlazorComponentsModule),
        typeof(DigniteAbpBlazoriseUIModule)
        )]
    public class CmsAdminBlazorModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CmsAdminBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<CmsAdminBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new CmsAdminMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(CmsAdminBlazorModule).Assembly);
            });
            context.Services.AddAntDesign();
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        CmsModuleExtensionConsts.ModuleName,
                        CmsModuleExtensionConsts.EntityNames.Page,
                        createFormTypes: new[] { typeof(PageCreateDto) },
                        editFormTypes: new[] { typeof(PageUpdateDto) }
                    );

            });
        }
    }
}