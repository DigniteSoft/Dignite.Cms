using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dignite.SiteBuilding.EntityFrameworkCore
{
    public class SiteBuildingHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<SiteBuildingHttpApiHostMigrationsDbContext>
    {
        public SiteBuildingHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SiteBuildingHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("SiteBuilding"));

            return new SiteBuildingHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
