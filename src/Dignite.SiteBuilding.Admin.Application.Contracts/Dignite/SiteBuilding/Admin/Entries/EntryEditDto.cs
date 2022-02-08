using System;
using System.ComponentModel.DataAnnotations;
using Dignite.Abp.FieldCustomizing;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Dignite.SiteBuilding.Entries;
using Dignite.SiteBuilding.Admin.Sections;

namespace Dignite.SiteBuilding.Admin.Entries
{
    public class EntryEditDto: CustomizableObject
    {
        public EntryEditDto()
        {
            this.CustomizedFields = new CustomizeFieldDictionary();
        }

        public EntryEditDto(Guid id,Guid sectionId) :this()
        {
            Id = id;
            SectionId = sectionId;
            PublishTime = DateTime.Now;
            IsActive = true;
        }


        [Required]
        public Guid Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid SectionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid PageId { get; set; }


        /// <summary>
        /// 是否激活
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// Normalized Name
        /// </summary>
        [StringLength(EntryConsts.MaxSlugLength)]
        [RegularExpression(EntryConsts.SlugRegularExpression)]
        public virtual string Slug { get; set; }


        /// <summary>
        /// Get the definition information of the field for data validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public override IReadOnlyList<BasicCustomizeFieldDefinition> GetFieldDefinitions(ValidationContext validationContext)
        {
            var _sectionAppService = validationContext.GetRequiredService<ISectionAppService>();
            var section = _sectionAppService.GetAsync(SectionId).Result;
            return section.FieldDefinitions
                .Select(fd => new BasicCustomizeFieldDefinition(
                        fd.Name,
                        fd.DisplayName,
                        fd.FieldControlProviderName,
                        fd.DefaultValue,
                        fd.Configuration
                        )).ToList();

        }
    }
}
