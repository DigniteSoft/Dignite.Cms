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
        public async Task<ListResultDto<PageDto>> GetListAsync()
        {
            return await _siteAppService.GetListAsync();
        }


        [HttpGet]
        [Route("{id}/edit")]
        public async Task<PageEditOutput> EditAsync(Guid id)
        {
            return await _siteAppService.EditAsync(id);
        }


        [HttpPost]
        public async Task CreateAsync(PageEditDto edit)
        {
            await _siteAppService.CreateAsync(edit);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, PageEditDto edit)
        {
            await _siteAppService.UpdateAsync(id,edit);
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
