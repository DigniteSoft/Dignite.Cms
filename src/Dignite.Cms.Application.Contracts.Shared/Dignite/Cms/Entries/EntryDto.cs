using Dignite.Abp.FieldCustomizing;
using Dignite.Cms.Pages;
using Dignite.Cms.Users;
using Newtonsoft.Json;
using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Entries
{
    /// <summary>
    /// 条目列表
    /// </summary>
    [Serializable]
    public class EntryDto : FullAuditedEntityDto<Guid>, IHasCustomizableFields
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Guid SectionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid PageId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual string Title { get; set; }

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

        public SiteUserDto Editor { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PageDto Page { get; set; }
    }
}
