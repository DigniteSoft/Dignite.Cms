using Dignite.SiteBuilding.Pages;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding.Admin.Pages
{
    public interface IPageAppService : IApplicationService
    {
        Task<ListResultDto<PageDto>> GetListAsync();

        Task<PageEditOutput> EditAsync(Guid id);

        Task CreateAsync(PageEditDto edit);

        Task UpdateAsync(Guid id, PageEditDto edit);

        Task DeleteAsync(Guid id);

        Task MoveAsync(Guid id,MovePageInput input);
    }
}
