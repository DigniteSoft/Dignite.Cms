
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding.Admin.Blazor.WebAssembly
{
    [DependsOn(
        typeof(SiteBuildingAdminBlazorModule),
        typeof(SiteBuildingAdminHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class SiteBuildingAdminBlazorWebAssemblyModule : AbpModule
    {
        
    }
}