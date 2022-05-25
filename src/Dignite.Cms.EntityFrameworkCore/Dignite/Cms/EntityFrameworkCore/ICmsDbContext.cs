using Dignite.Cms.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.EntityFrameworkCore
{
    [ConnectionStringName(CmsDbProperties.ConnectionStringName)]
    public interface ICmsDbContext : IEfCoreDbContext
    {
        DbSet<SiteUser> Users { get; }
        DbSet<Pages.Page> Pages { get; }
        DbSet<Sections.Section> Sections { get; }
        DbSet<Sections.SectionGrant> SectionGrants { get; }
        DbSet<Sections.FieldDefinition> FieldDefinitions { get; }
        DbSet<Entries.Entry> Entries { get; }
    }
}