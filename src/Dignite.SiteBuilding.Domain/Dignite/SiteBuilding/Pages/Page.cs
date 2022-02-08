using Dignite.SiteBuilding.Entries;
using Dignite.SiteBuilding.Sections;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.SiteBuilding.Pages
{
    /// <summary>
    /// Site page
    /// </summary>
    public class Page : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Page(Guid id, Guid? parentId, bool isActive, string title, string name, Guid? tenantId )
        {
            Id = id;
            ParentId = parentId;
            IsActive = isActive;
            Title = title;
            Path = name;
            TenantId = tenantId;
        }

        protected Page()
        {
        }

        /// <summary>
        /// TenantId of this page.
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }


        /// <summary>
        /// Title of this page.
        /// </summary>
        [NotNull]
        public virtual string Title { get; set; }

        /// <summary>
        /// Path of this page.
        /// </summary>
        [NotNull]
        public virtual string Path { get; set; }


        /// <summary>
        /// Description of this page
        /// </summary>
        [CanBeNull]
        public string Description { get; set; }


        /// <summary>
        /// Keywords of this page
        /// </summary>
        [CanBeNull]
        public string[] Keywords { get; set; }

        /// <summary>
        /// Template file of this page
        /// </summary>
        public string TemplateFile { get; set; }

        /// <summary>
        /// A permission name. This page will be accessible to a user if this have permission.
        /// Optional.
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Position { get; private set; }


        public virtual ICollection<Entry> Entries { get; protected set; }

        public void SetPosition(int position)
        {
            this.Position = position;
        }
    }
}
