using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.SiteBuilding.MongoDB
{
    [ConnectionStringName(SiteBuildingDbProperties.ConnectionStringName)]
    public interface ISiteBuildingMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
