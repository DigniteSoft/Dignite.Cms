using System;
using System.Threading.Tasks;
using Dignite.Cms.Pages;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Pages
{
    [RemoteService(Name = CmsAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsAdminRemoteServiceConsts.ModuleName)]
    [Route("api/cms-admin/pages")]
    [ControllerName("PageManagement")]
    public class PageController : CmsAdminController, IPageAppService
    {
        private readonly IPageAppService _pageAppService;

        public PageController(IPageAppService siteAppService)
        {
            _pageAppService = siteAppService;
        }


        [HttpGet]
        [Route("{id}")]
        public virtual async Task<PageDto> GetAsync(Guid id)
        {
            return await _pageAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public virtual async Task<ListResultDto<PageDto>> GetAllListAsync()
        {
            return await _pageAppService.GetAllListAsync();
        }


        [HttpGet]
        public async Task<PagedResultDto<PageDto>> GetListAsync(GetPagesInput input)
        {
            return await _pageAppService.GetListAsync(input);
        }



        [HttpPost]
        public async Task<PageDto> CreateAsync(PageCreateDto edit)
        {
            return await _pageAppService.CreateAsync(edit);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<PageDto> UpdateAsync(Guid id, PageUpdateDto edit)
        {
            return await _pageAppService.UpdateAsync(id,edit);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _pageAppService.DeleteAsync(id);
        }

        [HttpPut]
        [Route("{id}/move")]
        public async Task MoveAsync(Guid id, MovePageInput input)
        {
            await _pageAppService.MoveAsync(id, input);
        }
    }
}
