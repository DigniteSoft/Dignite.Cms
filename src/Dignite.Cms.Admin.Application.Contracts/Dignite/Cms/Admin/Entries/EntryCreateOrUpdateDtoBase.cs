using Dignite.Abp.FieldCustomizing;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Entries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.Cms.Admin.Entries
{
    public abstract class EntryCreateOrUpdateDtoBase: CustomizableObject
    {
        public EntryCreateOrUpdateDtoBase():base()
        {
        }

        public EntryCreateOrUpdateDtoBase( Guid sectionId) : this()
        {
            SectionId = sectionId;
            PublishTime = DateTime.Now;
            IsActive = true;
        }



        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid SectionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual Guid PageId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual string Title { get; set; }

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
                        fd.FieldProviderName,
                        fd.DefaultValue,
                        fd.Configuration
                        )).ToList();

        }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationErrors = new List<ValidationResult>();
            if (PageId.Equals(Guid.Empty))
            {
                validationErrors.Add(new ValidationResult("请选择页面！"));
            }

            /*
             2022年6月5日注释。在BlazorServer模式下创建条目失败
             */
           // validationErrors.AddRange(base.Validate(validationContext));

            return validationErrors;
        }
    }
}
