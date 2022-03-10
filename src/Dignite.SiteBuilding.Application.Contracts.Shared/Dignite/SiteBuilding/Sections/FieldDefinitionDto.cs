using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;
using System;

namespace Dignite.SiteBuilding.Sections
{
    public class FieldDefinitionDto : ICustomizeFieldDefinition
    {
        public Guid Id { get; set; }

        public virtual Guid SectionId { get; set; }


        /// <summary>
        /// Display name of this field.
        /// </summary>
        [NotNull]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Unique Name
        /// </summary>
        [NotNull]
        public virtual string Name { get; set; }

        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        public string FieldControlProviderName { get; set; }

        [NotNull]
        public virtual FieldControlConfigurationDictionary Configuration { get; set; }
    }
}
