
namespace Dignite.SiteBuilding.Pages
{
    public static class PageConsts
    {
        /// <summary>
        /// Maximum length of the page title property.
        /// Default value: 64
        /// </summary>
        public const int MaxTitleLength = 64;


        /// <summary>
        /// Maximum depth of an page hierarchy.
        /// </summary>
        public const int MaxDepth = 5;

        /// <summary>
        /// Length of a name between backslashs.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Maximum length of the path property.
        /// </summary>
        public const int MaxPathLength = MaxDepth * (MaxNameLength + 1) - 1;

        /// <summary>
        /// Regular Expression of the Name property.
        /// </summary>
        public const string NameRegularExpression = "^[a-zA-Z][A-Za-z0-9_-]+$";

        /// <summary>
        /// Default value: 256
        /// </summary>
        public const int MaxDescriptionLength = 256;

        /// <summary>
        /// Maximum length of the page Keywords property.
        /// Default value: 256
        /// </summary>
        public const int MaxKeywordsLength = 64;

        /// <summary>
        /// Maximum length of the page template file property.
        /// Default value: 256
        /// </summary>
        public const int MaxTemplateFileLength = 256;

        /// <summary>
        /// Regular Expression of the template file path property.
        /// </summary>
        public const string TemplateFileRegularExpression = "^[a-zA-Z][A-Za-z0-9_.-]+$";

        /// <summary>
        /// Maximum length of the page permission name property.
        /// Default value: 128
        /// </summary>
        public const int MaxPermissionNameLength = 128;

    }
}
