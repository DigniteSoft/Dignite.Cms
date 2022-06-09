
namespace Dignite.Cms.Entries
{
    public static class EntryConsts
    {
        /// <summary>
        /// Maximum length of the entity 128 property.
        /// </summary>
        public const int MaxTitleLength = 128;

        /// <summary>
        /// Maximum length of the entity slug property.
        /// </summary>
        public const int MaxSlugLength = 128;


        /// <summary>
        /// Regular Expression of the Name property.
        /// </summary>
        public const string SlugRegularExpression = "^[a-zA-Z][A-Za-z0-9_.-]+$";

    }
}
