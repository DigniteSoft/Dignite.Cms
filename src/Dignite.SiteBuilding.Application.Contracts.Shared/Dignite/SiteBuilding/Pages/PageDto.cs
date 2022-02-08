using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Pages
{
    public class PageDto: FullAuditedEntityDto<Guid>
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
    }
}