using Dignite.SiteBuilding.Pages;
using System.ComponentModel.DataAnnotations;

namespace Dignite.SiteBuilding.Admin.Pages
{
    public class PageEditOutput : PageEditDto
    {
        /// <summary>
        /// Page path
        /// </summary>
        [Required]
        [StringLength(PageConsts.MaxNameLength)]
        [RegularExpression(PageConsts.NameRegularExpression)]
        public override string Name
        {
            get
            {
                return Path.Substring(Path.LastIndexOf('/') + 1);
            }
        }

        public virtual string Path { get; set; }
    }
}
