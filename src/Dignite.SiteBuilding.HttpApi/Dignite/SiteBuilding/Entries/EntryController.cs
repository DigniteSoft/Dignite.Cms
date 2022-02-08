using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Entries
{
    [RemoteService(Name = SiteBuildingRemoteServiceConsts.RemoteServiceName)]
    [Area(SiteBuildingRemoteServiceConsts.ModuleName)]
    [Route("api/site-building/entries")]
    public class EntryController : SiteBuildingController, IEntryAppService
    {
        private readonly IEntryAppService _entryAppService;

        public EntryController(IEntryAppService entryAppService)
        {
            _entryAppService = entryAppService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<EntryDto> GetAsync(Guid id)
        {
            return await _entryAppService.GetAsync(id);
        }


        [HttpGet]
        [Route("find-by-slug/{pageId}/{slug}")]
        public async Task<EntryDto> FindBySlugAsync(Guid pageId, string slug)
        {
            return await _entryAppService.FindBySlugAsync(pageId, slug);
        }

        /// <summary>
        /// 获取上一条条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/prev")]
        public async Task<EntryDto> FindPrevAsync(Guid id)
        {
            return await _entryAppService.FindPrevAsync(id);
        }

        /// <summary>
        /// 获取下一条条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/next")]
        public async Task<EntryDto> FindNextAsync(Guid id)
        {
            return await _entryAppService.FindNextAsync(id);
        }



        /// <summary>
        /// 查询条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            return await _entryAppService.GetListAsync(input);
        }
    }
}
