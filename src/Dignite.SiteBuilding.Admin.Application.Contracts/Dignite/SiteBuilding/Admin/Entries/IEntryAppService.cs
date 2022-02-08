
using Dignite.SiteBuilding.Entries;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding.Admin.Entries
{
    public interface IEntryAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task<EntryEditOutput> NewAsync(Guid sectionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EntryEditOutput> EditAsync(Guid id);

        /// <summary>
        /// 创建或更新条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        Task CreateAsync(EntryEditDto input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(EntryEditDto input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input);

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
