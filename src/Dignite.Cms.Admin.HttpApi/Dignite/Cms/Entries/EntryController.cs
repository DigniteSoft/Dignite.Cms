using Dignite.Cms.Entries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Entries
{
    [RemoteService(Name = CmsAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsAdminRemoteServiceConsts.ModuleName)]
    [Route("api/cms-admin/entry")]
    [ControllerName("EntryManagement")]
    public class EntryController : CmsAdminController, IEntryAppService
    {
        private readonly IEntryAppService _entryAppService;

        public EntryController(IEntryAppService entryAppService)
        {
            _entryAppService = entryAppService;
        }


        [HttpGet]
        [Route("new")]
        public async Task<NewEntryOutput> NewAsync(Guid sectionId)
        {
            return await _entryAppService.NewAsync(sectionId);
        }


        [HttpGet]
        [Route("{id}/edit")]
        public async Task<EditEntryOutput> EditAsync(Guid id)
        {
            return await _entryAppService.EditAsync(id);
        }

        /// <summary>
        /// 创建或更新条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>


        [HttpPost]
        public async Task<EntryDto> CreateAsync(EntryCreateDto input)
        {
            return await _entryAppService.CreateAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPut]
        public async Task<EntryDto> UpdateAsync(Guid id,EntryUpdateDto input)
        {
            return await _entryAppService.UpdateAsync(id,input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _entryAppService.DeleteAsync(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            return await _entryAppService.GetListAsync(input);
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


        /// <summary>
        /// 获取条目选择字段的数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("field-source")]
        public async Task<PagedResultDto<EntryDto>> GetFieldSourceAsync(GetEntryChoiceFieldSourceInput input)
        {
            return await _entryAppService.GetFieldSourceAsync(input);
        }

        /// <summary>
        /// 移动条目位置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetParentId"></param>
        /// <param name="afterEntryId"></param>
        /// <returns></returns>

        [HttpPut]
        [Route("move")]
        public async Task MoveAsync(MoveEntriesInput input)
        {
            await _entryAppService.MoveAsync(input);
        }

        /// <summary>
        /// 审核内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAllow"></param>
        /// <returns></returns>

        [HttpPut]
        [Route("audit")]
        public async Task AuditAsync(AuditEntriesInput input)
        {
            await _entryAppService.AuditAsync(input);
        }
    }
}
