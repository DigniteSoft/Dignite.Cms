using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.SiteBuilding.Admin.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(SiteBuildingAdminBlazorModule)
        )]
    public class SiteBuildingAdminBlazorServerModule : AbpModule
    {
        
    }
}