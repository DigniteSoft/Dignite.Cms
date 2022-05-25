using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Dignite.Abp.FieldCustomizing.EntityFrameworkCore;

namespace Dignite.Cms.EntityFrameworkCore
{
    [DependsOn(
        typeof(CmsDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(DigniteAbpFieldCustomizingEntityFrameworkCoreModule)
    )]
    public class CmsEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CmsDbContext>(options =>
            {
                options.AddRepository<Users.SiteUser, Users.EfCoreSiteUserRepository>();
                options.AddRepository<Pages.Page, Pages.EfCorePageRepository>();
                options.AddRepository<Sections.SectionGrant, Sections.EfCoreSectionGrantRepository>();
                options.AddRepository<Sections.Section, Sections.EfCoreSectionRepository>();
                options.AddRepository<Sections.FieldDefinition, Sections.EfCoreFieldDefinitionRepository>();
                options.AddRepository<Entries.Entry, Entries.EfCoreEntryRepository>();
            });
        }
    }
}