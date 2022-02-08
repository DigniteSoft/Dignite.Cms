using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Dignite.SiteBuilding.MongoDB
{
    public class SiteBuildingMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public SiteBuildingMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}