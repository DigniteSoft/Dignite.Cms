using Dignite.SiteBuilding.Sections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Admin.Sections
{
    [RemoteService(Name = SiteBuildingAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(SiteBuildingAdminRemoteServiceConsts.ModuleName)]
    [Route("api/site-building-admin/sections")]
    [ControllerName("SectionManagement")]
    public class SectionController : SiteBuildingAdminController, ISectionAppService
    {
        private readonly ISectionAppService _sectionAppService;

        public SectionController(ISectionAppService sectionAppService)
        {
            _sectionAppService = sectionAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("new")]
        public async Task<SectionEditOutput> NewAsync()
        {
            return await _sectionAppService.NewAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{id}/edit")]
        public async Task<SectionEditOutput> EditAsync(Guid id)
        {
            return await _sectionAppService.EditAsync(id);
        }


        [HttpPost]
        public async Task CreateAsync(SectionEditDto input)
        {
            await _sectionAppService.CreateAsync(input);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, SectionEditDto input)
        {
            await _sectionAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _sectionAppService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<SectionDto>> GetListAsync(GetSectionsInput input)
        {
            return await _sectionAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<SectionDto> GetAsync(Guid id)
        {
            return await _sectionAppService.GetAsync(id);
        }


        /// <summary>
        /// 授权人员管理条目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/authorizer")]
        public async Task AddAuthorizerAsync(Guid id, AuthorizerEditInput input)
        {
            await _sectionAppService.AddAuthorizerAsync(id,input);
        }

        /// <summary>
        /// 更新授权人员管理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/authorizer")]
        public async Task UpdateAuthorizerAsync(Guid id, AuthorizerEditInput input)
        {
            await _sectionAppService.UpdateAuthorizerAsync(id,input);
        }

        /// <summary>
        /// 移除授权管理人员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/authorizer")]
        public async Task RemoveAuthorizerAsync(Guid id, Guid userId)
        {
            await _sectionAppService.RemoveAuthorizerAsync(id, userId);
        }

        /// <summary>
        /// 获取授权的管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/authorizers")]
        public async Task<ListResultDto<SectionAuthorizerDto>> GetAuthorizersAsync(Guid id)
        {
            return await _sectionAppService.GetAuthorizersAsync(id);
        }
    }
}
