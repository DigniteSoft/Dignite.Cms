using Dignite.Abp.FieldCustomizing;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Cms.Entries
{
    public class Entry: FullAuditedEntity<Guid>, IHasCustomizableFields, IMultiTenant
    {
        protected Entry()
        {     
        }

        public Entry(
            Guid id, 
            Guid sectionId,
            Guid pageId,
            bool isActive,
            string slug,
            DateTime publishTime,
            Guid? tenantId
            )
        {
            this.Id = id;
            this.SectionId = sectionId;
            this.PageId = pageId;
            this.IsActive = isActive;
            this.Slug = slug;
            this.PublishTime = publishTime;
            this.CustomizedFields = new CustomizeFieldDictionary();
            this.AuditStatus = EntryAuditStatus.Waiting;       
            this.TenantId = tenantId;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid SectionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid PageId { get; set;}


        /// <summary>
        /// 审核情况
        /// </summary>
        public virtual EntryAuditStatus AuditStatus { get; protected set; }

        /// <summary>
        /// 是否为有效的条目
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public virtual DateTime PublishTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// 排序值；
        /// </summary>
        public virtual int Position { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public CustomizeFieldDictionary CustomizedFields { get; set; }


        public virtual Guid? TenantId { get; protected set; }



        public virtual void SetAuditStatus(EntryAuditStatus auditStatus)
        {
            this.AuditStatus = auditStatus;
        }

        public virtual void SetPosition(int position)
        {
            this.Position = position;
        }

    }
}
