using System;

namespace Dignite.Cms.Admin.Pages
{
    /// <summary>
    /// 移动页面的条件
    /// </summary>
    public class MovePageInput
    {
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 移动到该页面的后面
        /// </summary>
        public Guid? BeforId { get; set; }
    }
}
