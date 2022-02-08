using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.AspNetCore.Authorization;
using Dignite.SiteBuilding.Admin.Entries;

namespace Dignite.SiteBuilding.Admin
{
    [DependsOn(
        typeof(SiteBuildingDomainModule),
        typeof(SiteBuildingAdminApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class SiteBuildingAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<SiteBuildingAdminApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SiteBuildingAdminApplicationModule>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("DigniteSiteBuildingCreatePolicy", policy => policy.Requirements.Add(CommonOperations.Create));
                options.AddPolicy("DigniteSiteBuildingUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
                options.AddPolicy("DigniteSiteBuildingDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
            });

            context.Services.AddSingleton<IAuthorizationHandler, EntryAuthorizationHandler>();
        }
    }
}
