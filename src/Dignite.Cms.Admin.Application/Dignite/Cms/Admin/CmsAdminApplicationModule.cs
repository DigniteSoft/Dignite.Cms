using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.AspNetCore.Authorization;
using Dignite.Cms.Admin.Entries;

namespace Dignite.Cms.Admin
{
    [DependsOn(
        typeof(CmsDomainModule),
        typeof(CmsAdminApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class CmsAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CmsAdminApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CmsAdminApplicationModule>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("DigniteCmsCreatePolicy", policy => policy.Requirements.Add(CommonOperations.Create));
                options.AddPolicy("DigniteCmsUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
                options.AddPolicy("DigniteCmsDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
            });

            context.Services.AddSingleton<IAuthorizationHandler, EntryAuthorizationHandler>();
        }
    }
}
