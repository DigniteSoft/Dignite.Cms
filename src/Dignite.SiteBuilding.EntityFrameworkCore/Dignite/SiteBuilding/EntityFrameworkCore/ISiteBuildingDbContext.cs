using Dignite.SiteBuilding.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.SiteBuilding.EntityFrameworkCore
{
    [ConnectionStringName(SiteBuildingDbProperties.ConnectionStringName)]
    public interface ISiteBuildingDbContext : IEfCoreDbContext
    {
        DbSet<SiteUser> Users { get; }
        DbSet<Pages.Page> Pages { get; }
        DbSet<Sections.Section> Sections { get; }
        DbSet<Sections.SectionGrant> SectionGrants { get; }
        DbSet<Sections.FieldDefinition> FieldDefinitions { get; }
        DbSet<Entries.Entry> Entries { get; }
    }
}