using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Cms.Sections
{
    [RemoteService(Name = CmsRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsRemoteServiceConsts.ModuleName)]
    [Route("api/cms/sections")]
    public class SectionController : CmsController, ISectionAppService
    {
        private readonly ISectionAppService _sectionAppService;

        public SectionController(ISectionAppService sectionAppService)
        {
            _sectionAppService = sectionAppService;
        }

        [HttpGet]
        [Route("find-by-name/{name}")]
        public async Task<SectionDto> FindByNameAsync( string name)
        {
            return await _sectionAppService.FindByNameAsync( name);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<SectionDto> GetAsync(Guid id)
        {
            return await _sectionAppService.GetAsync(id);
        }
    }
}
