using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Cms.Sections;
using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Sections
{
    public class FieldDefinitionEditDto: ICustomizeFieldDefinition
    {
        [Required]
        public Guid Id { get; set; }

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
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [Required]
        [StringLength(FieldDefinitionConsts.MaxFieldProviderNameLength)]
        public string FieldProviderName { get; set; }

        [Required]
        public virtual FieldConfigurationDictionary Configuration { get; set; }
    }
}
