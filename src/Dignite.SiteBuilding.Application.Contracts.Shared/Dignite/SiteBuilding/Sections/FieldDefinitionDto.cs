using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;
using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Sections
{
    public class FieldDefinitionDto : EntityDto<Guid>
    {
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
        /// Description
        /// </summary>
        [CanBeNull]
        public string Description { get; set; }

        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        public string FieldControlProviderName { get; }

        [NotNull]
        public virtual FieldControlConfigurationDictionary Configuration { get; set; }
    }
}
