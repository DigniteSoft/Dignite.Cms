
using Dignite.Cms.Entries;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.Entries
{
    public interface IEntryAppService
    : ICrudAppService<
        EntryDto,
        Guid,
        GetEntriesInput,
        EntryCreateDto,
        EntryUpdateDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task<NewEntryOutput> NewAsync(Guid sectionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditEntryOutput> EditAsync(Guid id);

        /// <summary>
        /// 获取条目选择字段的数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EntryDto>> GetFieldSourceAsync(GetEntryChoiceFieldSourceInput input);

        /// <summary>
        /// 移动条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task MoveAsync(MoveEntriesInput input);

        /// <summary>
        /// 审核条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AuditAsync(AuditEntriesInput input);
    }
}
