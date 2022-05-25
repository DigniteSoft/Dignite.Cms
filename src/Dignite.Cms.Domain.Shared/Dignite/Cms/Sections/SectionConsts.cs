

namespace Dignite.Cms.Sections
{
    public static class SectionConsts
    {
        /// <summary>
        /// Maximum length of the entity type display name property.
        /// Default value: 64
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Maximum length of the entity type name property.
        /// Default value: 64
        /// </summary>
        public const int MaxNameLength = 64;

        /// <summary>
        /// Regular Expression of the entity type name property.
        /// </summary>
        public const string NameRegularExpression = "^[a-zA-Z][A-Za-z0-9_-]+$";


        /// <summary>
        /// Regular Expression of the template file path property.
        /// </summary>
        public const string TemplateFileRegularExpression = "^[a-zA-Z][A-Za-z0-9_.-]+$";
    }
}
