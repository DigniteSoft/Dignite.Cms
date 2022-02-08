using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Sections
{
    /// <summary>
    /// 版块中条目类型
    /// </summary> 
    [Serializable] 
    public class SectionDto : EntityDto<Guid>
    {
        /// <summary>
        /// Display Name of this entry type.
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Name of this entry type.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Template file of this entry type
        /// </summary>
        public string TemplateFile { get; set; }

        /// <summary>
        /// Template file of this entry
        /// </summary>
        public string EntryTemplateFile { get; set; }


        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }


        public virtual ICollection<FieldDefinitionDto> FieldDefinitions { get; protected set; }
    }
}
