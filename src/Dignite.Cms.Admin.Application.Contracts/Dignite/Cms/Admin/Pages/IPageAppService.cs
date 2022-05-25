using Dignite.Cms.Pages;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.Pages
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
