using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding.Entries
{
    public interface IEntryAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EntryDto> GetAsync(Guid id);


        Task<EntryDto> FindBySlugAsync(Guid pageId,string slug);

        /// <summary>
        /// 获取上一条条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EntryDto> FindPrevAsync(Guid id);

        /// <summary>
        /// 获取下一条条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EntryDto> FindNextAsync(Guid id);

        /// <summary>
        /// 获取条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input);
    }
}
