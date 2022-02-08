using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.SiteBuilding.Admin.Sections
{
    public class AuthorizerEditInput
    {
        [Required]
        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// 授权管理员的页面
        /// </summary>
        [Required]
        public virtual Guid[] PageIds { get; set; }
    }
}
