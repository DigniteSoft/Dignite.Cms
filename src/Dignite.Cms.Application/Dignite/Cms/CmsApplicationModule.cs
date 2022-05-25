using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.AspNetCore.Authorization;
using Dignite.Cms.Pages;

namespace Dignite.Cms
{
    [DependsOn(
        typeof(CmsDomainModule),
        typeof(CmsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class CmsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CmsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CmsApplicationModule>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("DigniteCmsReadPolicy", policy => policy.Requirements.Add(CommonOperations.Read));
            });

            context.Services.AddSingleton<IAuthorizationHandler, PageAuthorizationHandler>();
        }
    }
}
