using System;
using System.Threading.Tasks;
using Dignite.SiteBuilding.Pages;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Admin.Pages
{
    [RemoteService(Name = SiteBuildingAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(SiteBuildingAdminRemoteServiceConsts.ModuleName)]
    [Route("api/site-building-admin/pages")]
    [ControllerName("PageManagement")]
    public class PageController : SiteBuildingAdminController, IPageAppService
    {
        private readonly IPageAppService _siteAppService;

        public PageController(IPageAppService siteAppService)
        {
            _siteAppService = siteAppService;
        }


        [HttpGet]
        [Route("{id}")]
        public virtual async Task<PageDto> GetAsync(Guid id)
        {
            return await _siteAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public virtual async Task<ListResultDto<PageDto>> GetAllListAsync()
        {
            return await _siteAppService.GetAllListAsync();
        }


        [HttpGet]
        public async Task<PagedResultDto<PageDto>> GetListAsync(GetPagesInput input)
        {
            return await _siteAppService.GetListAsync(input);
        }



        [HttpPost]
        public async Task<PageDto> CreateAsync(PageCreateDto edit)
        {
            return await _siteAppService.CreateAsync(edit);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<PageDto> UpdateAsync(Guid id, PageUpdateDto edit)
        {
            return await _siteAppService.UpdateAsync(id,edit);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _siteAppService.DeleteAsync(id);
        }

        [HttpPut]
        [Route("{id}/move")]
        public async Task MoveAsync(Guid id, MovePageInput input)
        {
            await _siteAppService.MoveAsync(id, input);
        }
    }
}
