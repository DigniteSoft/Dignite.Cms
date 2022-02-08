using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.SiteBuilding.MongoDB
{
    [ConnectionStringName(SiteBuildingDbProperties.ConnectionStringName)]
    public class SiteBuildingMongoDbContext : AbpMongoDbContext, ISiteBuildingMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureSiteBuilding();
        }
    }
}