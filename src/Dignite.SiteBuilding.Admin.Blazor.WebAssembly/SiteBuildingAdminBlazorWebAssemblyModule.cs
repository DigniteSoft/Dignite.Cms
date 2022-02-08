using Dignite.Abp.AntDesignBlazorUI.WebAssembly;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding.Admin.Blazor.WebAssembly
{
    [DependsOn(
        typeof(SiteBuildingAdminBlazorModule),
        typeof(SiteBuildingAdminHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(DigniteAbpAntDesignBlazorUIWebAssemblyModule)
        )]
    public class SiteBuildingAdminBlazorWebAssemblyModule : AbpModule
    {
        
    }
}