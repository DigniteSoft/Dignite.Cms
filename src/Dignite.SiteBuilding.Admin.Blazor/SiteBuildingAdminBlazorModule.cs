﻿using Microsoft.Extensions.DependencyInjection;
using Dignite.SiteBuilding.Admin.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Dignite.Abp.AntDesignBlazorUI;

namespace Dignite.SiteBuilding.Admin.Blazor
{
    [DependsOn(
        typeof(SiteBuildingAdminApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule),
        typeof(DigniteAbpAntDesignBlazorUIModule)
        )]
    public class SiteBuildingAdminBlazorModule : AbpModule
    {
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

            context.Services.AddAntDesign();
        }
    }
}