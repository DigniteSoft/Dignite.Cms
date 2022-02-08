using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.SiteBuilding.Sections
{
    [RemoteService(Name = SiteBuildingRemoteServiceConsts.RemoteServiceName)]
    [Area(SiteBuildingRemoteServiceConsts.ModuleName)]
    [Route("api/site-building/sections")]
    public class SectionController : SiteBuildingController, ISectionAppService
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
