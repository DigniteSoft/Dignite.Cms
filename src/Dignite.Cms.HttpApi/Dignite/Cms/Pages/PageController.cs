using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Pages
{
    [RemoteService(Name = CmsRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsRemoteServiceConsts.ModuleName)]
    [Route("api/cms/pages")]
    public class PageController : CmsController, IPageAppService
    {
        private readonly IPageAppService _pageAppService;

        public PageController(IPageAppService pageAppService)
        {
            _pageAppService = pageAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<PageDto> GetAsync(Guid id)
        {
            return await _pageAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("find-by-path")]
        public async Task<PageDto> FindByPathAsync(string path)
        {
            return await _pageAppService.FindByPathAsync(path);
        }

        [HttpGet]
        public async Task<ListResultDto<PageDto>> GetListAsync()
        {
            return await _pageAppService.GetListAsync();
        }
    }
}
