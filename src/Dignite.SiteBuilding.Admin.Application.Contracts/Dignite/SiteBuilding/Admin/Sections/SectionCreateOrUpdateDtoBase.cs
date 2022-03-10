using Dignite.SiteBuilding.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.SiteBuilding.Admin.Sections
{
    public abstract class SectionCreateOrUpdateDtoBase : IValidatableObject
    {
        protected SectionCreateOrUpdateDtoBase()
        {
            this.FieldDefinitions = new List<FieldDefinitionEditDto>();
        }


        /// <summary>
        /// Display Name of this entity type.
        /// </summary>
        [Required]
        [StringLength(SectionConsts.MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 版块下条目类型唯一名称；
        /// </summary>
        [Required]
        [StringLength(SectionConsts.MaxNameLength)]
        [RegularExpression(SectionConsts.NameRegularExpression)]
        public virtual string Name { get; set; }


        /// <summary>
        /// Template file of this entity type
        /// </summary>
        [RegularExpression(SectionConsts.TemplateFileRegularExpression)]
        public string TemplateFile { get; set; }

        /// <summary>
        /// Template file of this entity
        /// </summary>
        [RegularExpression(SectionConsts.TemplateFileRegularExpression)]
        public string EntryTemplateFile { get; set; }



        /// <summary>
        /// 是否激活
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Field definitions
        /// </summary>
        public virtual ICollection<FieldDefinitionEditDto> FieldDefinitions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationErrors = new List<ValidationResult>();

            foreach (var fieldDefinition in FieldDefinitions)
            {
                if (FieldDefinitions.Count(fd => fd.Name.Equals(fieldDefinition.Name, StringComparison.InvariantCultureIgnoreCase))>1)
                {
                    validationErrors.Add(
                      new ValidationResult(
                          $"{fieldDefinition.Name} 名称已存在！",
                          new[] { fieldDefinition.Name }
                          ));
                }
            }

            return validationErrors;
        }
    }
}
