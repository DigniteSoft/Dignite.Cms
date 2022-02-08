
namespace Dignite.SiteBuilding.Sections
{
    public static class FieldDefinitionConsts
    {
        /// <summary>
        /// Maximum length of the field display name property.
        /// Default value: 64
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Maximum length of the field name property.
        /// Default value: 64
        /// </summary>
        public const int MaxNameLength = 64;

        /// <summary>
        /// Regular Expression of the field Name property.
        /// </summary>
        public const string NameRegularExpression = "^[a-zA-Z][A-Za-z0-9_-]+$";

        /// <summary>
        /// Default value: 256
        /// </summary>
        public const int MaxDescriptionLength = 256;


        /// <summary>
        /// Maximum length of the field control provider name property.
        /// Default value: 128
        /// </summary>
        public const int MaxFieldControlProviderNameLength = 128;
    }
}
