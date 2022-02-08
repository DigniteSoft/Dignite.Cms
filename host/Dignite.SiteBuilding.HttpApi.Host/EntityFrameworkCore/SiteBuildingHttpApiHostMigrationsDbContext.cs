using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.SiteBuilding.EntityFrameworkCore
{
    public class SiteBuildingHttpApiHostMigrationsDbContext : AbpDbContext<SiteBuildingHttpApiHostMigrationsDbContext>
    {
        public SiteBuildingHttpApiHostMigrationsDbContext(DbContextOptions<SiteBuildingHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureSiteBuilding();
        }
    }
}
