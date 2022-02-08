
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding.Pages
{
    public interface IPageAppService : IApplicationService
    {
        /// <summary>
        /// 获取页面；
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PageDto> GetAsync(Guid id);
        Task<PageDto> FindByPathAsync(string path);

        Task<ListResultDto<PageDto>> GetListAsync();

    }
}
