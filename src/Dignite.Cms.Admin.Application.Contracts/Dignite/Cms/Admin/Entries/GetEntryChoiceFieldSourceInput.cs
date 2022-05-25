using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Entries
{
    /// <summary>
    /// 获取条目选择字段数据源的条件
    /// </summary>
    public class GetEntryChoiceFieldSourceInput: PagedResultRequestDto
    {
        public GetEntryChoiceFieldSourceInput()
        {
            MaxResultCount = 50;
        }

        [Required]
        public Guid FieldId { get; set; }
    }
}
