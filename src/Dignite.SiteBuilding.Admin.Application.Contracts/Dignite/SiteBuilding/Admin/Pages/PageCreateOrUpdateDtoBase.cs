using Dignite.SiteBuilding.Pages;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace Dignite.SiteBuilding.Admin.Pages
{
    public abstract class PageCreateOrUpdateDtoBase: ExtensibleObject
    {
        protected PageCreateOrUpdateDtoBase() : base(false)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public bool IsActive { get; set; }


        /// <summary>
        /// Title of this page.
        /// </summary>
        [Required] 
        [StringLength(PageConsts.MaxTitleLength)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Page path
        /// </summary>
        [Required]
        [StringLength(PageConsts.MaxNameLength)]
        [RegularExpression(PageConsts.NameRegularExpression)]
        public virtual string Name { get; set; }


        /// <summary>
        /// Description of this page
        /// </summary>
        [StringLength(PageConsts.MaxDescriptionLength)]
        public string Description { get; set; }


        /// <summary>
        /// Keywords of this page
        /// </summary>
        //[StringLength(PageConsts.MaxKeywordsLength)]
        public string[] Keywords { get; set; }

        /// <summary>
        /// Template file of this page
        /// </summary>
        [StringLength(PageConsts.MaxTemplateFileLength)]
        [RegularExpression(PageConsts.TemplateFileRegularExpression)]
        public string TemplateFile { get; set; }

        /// <summary>
        /// A permission name. This page will be accessible to a user if this have permission.
        /// Optional.
        /// </summary>
        [StringLength(PageConsts.MaxPermissionNameLength)]
        public string PermissionName { get; set; }
    }
}
