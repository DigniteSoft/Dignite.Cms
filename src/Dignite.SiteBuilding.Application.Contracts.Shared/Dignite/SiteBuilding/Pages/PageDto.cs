using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.SiteBuilding.Pages
{
    public class PageDto: ExtensibleFullAuditedEntityDto<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
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
        public virtual string Title { get; set; }

        /// <summary>
        /// Name of this page.
        /// </summary>
        public string Name {
            get {
                return Path.Substring(Path.LastIndexOf('/')+1);
            }
        }

        /// <summary>
        /// Path of this page.
        /// </summary>
        public virtual string Path { get; set; }


        /// <summary>
        /// Description of this page
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Keywords of this page
        /// </summary>
        public string[] Keywords { get; set; }

        /// <summary>
        /// Template file of this page
        /// </summary>
        public string TemplateFile { get; set; }

        public string PermissionName { get; set; }

        /// <summary>
        /// 子页面
        /// </summary>
        public ICollection<PageDto> Children { get; set; }


        public Guid? TenantId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}