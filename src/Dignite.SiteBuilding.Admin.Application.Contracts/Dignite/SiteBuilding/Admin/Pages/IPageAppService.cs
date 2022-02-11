using Dignite.SiteBuilding.Pages;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding.Admin.Pages
{
    public interface IPageAppService
    : ICrudAppService<
        PageDto,
        Guid,
        GetPagesInput,
        PageCreateDto,
        PageUpdateDto>
    {
        Task<ListResultDto<PageDto>> GetAllListAsync();

        Task MoveAsync(Guid id,MovePageInput input);
    }
}
