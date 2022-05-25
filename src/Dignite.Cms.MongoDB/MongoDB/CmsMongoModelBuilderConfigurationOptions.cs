using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Dignite.Cms.MongoDB
{
    public class CmsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public CmsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}