using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Cms.Admin.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(CmsAdminBlazorModule)
        )]
    public class CmsAdminBlazorServerModule : AbpModule
    {
        
    }
}