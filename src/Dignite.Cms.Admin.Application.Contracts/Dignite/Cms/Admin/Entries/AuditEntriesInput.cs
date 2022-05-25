using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Entries
{
    public class AuditEntriesInput
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid SectionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid[] Ids { get; set; }

        /// <summary>
        /// 是否通过审核
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}
