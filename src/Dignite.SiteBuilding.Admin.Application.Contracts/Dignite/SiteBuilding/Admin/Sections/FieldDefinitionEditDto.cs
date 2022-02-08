using Dignite.Abp.FieldCustomizing.FieldControls;
using Dignite.SiteBuilding.Sections;
using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.SiteBuilding.Admin.Sections
{
    public class FieldDefinitionEditDto
    {
        public Guid Id { get; set; }

        [Required]
        public virtual Guid SectionId { get; set; }

        /// <summary>
        /// Display name of this field.
        /// </summary>
        [Required]
        [StringLength(FieldDefinitionConsts.MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Unique Name
        /// </summary>
        [Required]
        [StringLength(FieldDefinitionConsts.MaxNameLength)]
        [RegularExpression(FieldDefinitionConsts.NameRegularExpression)]
        public virtual string Name { get; set; }


        /// <summary>
        /// Description
        /// </summary>
        [StringLength(FieldDefinitionConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [Required]
        [StringLength(FieldDefinitionConsts.MaxFieldControlProviderNameLength)]
        public string FieldControlProviderName { get; }

        [Required]
        public virtual FieldControlConfigurationDictionary Configuration { get; set; }

        public int Position { get; set; }
    }
}
