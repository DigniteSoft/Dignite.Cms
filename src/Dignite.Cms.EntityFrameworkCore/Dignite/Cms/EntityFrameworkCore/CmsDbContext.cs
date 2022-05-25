using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.EntityFrameworkCore
{
    [ConnectionStringName(CmsDbProperties.ConnectionStringName)]
    public class CmsDbContext : AbpDbContext<CmsDbContext>, ICmsDbContext
    {
        public DbSet<Users.SiteUser> Users { get; }
        public DbSet<Pages.Page> Pages { get; set; }
        public DbSet<Sections.Section> Sections { get; set; }
        public DbSet<Sections.SectionGrant> SectionGrants { get; set; }
        public DbSet<Sections.FieldDefinition> FieldDefinitions { get; set; }
        public DbSet<Entries.Entry> Entries { get; set; }

        public CmsDbContext(DbContextOptions<CmsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCms();
        }
    }
}