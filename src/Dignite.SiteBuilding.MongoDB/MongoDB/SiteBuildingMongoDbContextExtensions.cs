using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.SiteBuilding.MongoDB
{
    public static class SiteBuildingMongoDbContextExtensions
    {
        public static void ConfigureSiteBuilding(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new SiteBuildingMongoModelBuilderConfigurationOptions(
                SiteBuildingDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}