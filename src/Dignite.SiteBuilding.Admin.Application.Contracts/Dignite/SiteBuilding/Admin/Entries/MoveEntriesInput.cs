using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.SiteBuilding.Admin.Entries
{
    /// <summary>
    /// 移动条目的输入条件
    /// </summary>
    public class MoveEntriesInput
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
        /// 目标页面Id
        /// </summary>
        public Guid? TargetPageId { get; set; }

        /// <summary>
        /// 移动到该条目的后面
        /// </summary>
        public Guid? BeforId { get; set; }
    }
}
