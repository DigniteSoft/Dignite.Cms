using Dignite.Cms.Users;
using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Sections
{
    public class SectionAuthorizerDto: CreationAuditedEntityDto
    {
        public virtual Guid SectionId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        public SiteUserDto User { get; set; }

        /// <summary>
        /// 授权管理员的页面
        /// </summary>
        public virtual Guid[] PageIds { get; set; }
    }
}
