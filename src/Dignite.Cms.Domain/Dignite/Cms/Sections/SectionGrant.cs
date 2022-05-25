using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Cms.Sections
{
    /// <summary>
    /// 条目管理的授权人员
    /// </summary>
    public class SectionGrant: CreationAuditedEntity, IMultiTenant
    {
        public virtual Guid SectionId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// 授权管理员的页面
        /// </summary>
        public virtual Guid[] PageIds { get; set; }

        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionGrant"/> class.
        /// </summary>
        protected SectionGrant()
        {

        }

        public SectionGrant(Guid sectionId, Guid userId, Guid[] pageIds, Guid? tenantId)
        {
            SectionId = sectionId;
            UserId = userId;
            PageIds = pageIds;
            TenantId = tenantId;
        }

        public override object[] GetKeys()
        {
            return new object[] { SectionId, UserId };
        }
    }
}
