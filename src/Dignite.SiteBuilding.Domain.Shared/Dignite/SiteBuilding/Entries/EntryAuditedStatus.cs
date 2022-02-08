
namespace Dignite.SiteBuilding.Entries
{
    /// <summary>
    /// 条目审核状态
    /// </summary>
    public enum EntryAuditStatus
    {
        /// <summary>
        /// 等待审核
        /// </summary>
        Waiting = 1,

        /// <summary>
        /// 驳回的
        /// </summary>
        Disallowed = 2,

        /// <summary>
        /// 通过审核
        /// </summary>
        Allowed = 3
    }
}
