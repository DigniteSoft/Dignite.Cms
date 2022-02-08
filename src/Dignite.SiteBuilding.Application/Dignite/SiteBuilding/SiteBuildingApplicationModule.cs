using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.AspNetCore.Authorization;
using Dignite.SiteBuilding.Pages;

namespace Dignite.SiteBuilding
{
    [DependsOn(
        typeof(SiteBuildingDomainModule),
        typeof(SiteBuildingApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class SiteBuildingApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<SiteBuildingApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SiteBuildingApplicationModule>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("DigniteSiteBuildingReadPolicy", policy => policy.Requirements.Add(CommonOperations.Read));
            });

            context.Services.AddSingleton<IAuthorizationHandler, PageAuthorizationHandler>();
        }
    }
}
