
using Dignite.Abp.FieldCustomizing;
using Dignite.SiteBuilding.Pages;
using Dignite.SiteBuilding.Sections;
using Dignite.SiteBuilding.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Entries
{
    public class EntryAppService : SiteBuildingAppService, IEntryAppService
    {
        private readonly IPageAppService _pageAppService;
        private readonly IEntryRepository _entryRepository;
        protected ISiteUserLookupService UserLookupService { get; }
        private readonly ISectionRepository _sectionRepository;

        public EntryAppService(IPageAppService pageAppService, 
            IEntryRepository entryRepository, 
            ISiteUserLookupService userLookupService, 
            ISectionRepository sectionRepository)
        {
            _pageAppService = pageAppService;
            _entryRepository = entryRepository;
            UserLookupService = userLookupService;
            _sectionRepository = sectionRepository;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EntryDto> GetAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id);

            return await GetEntryDto(entry);
        }


        public async Task<EntryDto> FindBySlugAsync(Guid pageId, string slug)
        {
            var entry = await _entryRepository.FindBySlugAsync(pageId, slug);

            return await GetEntryDto(entry);
        }

        /// <summary>
        /// 获取上一条条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EntryDto> FindPrevAsync(Guid id)
        {
            var entry = await _entryRepository.FindPrevAsync(id);
            return await GetEntryDto(entry);
        }

        /// <summary>
        /// 获取下一条条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EntryDto> FindNextAsync(Guid id)
        {
            var entry = await _entryRepository.FindNextAsync(id);
            return await GetEntryDto(entry);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            var page = await _pageAppService.GetAsync(input.PageId);
            var section = await _sectionRepository.GetAsync(input.SectionId, true);

            var count = await _entryRepository.GetCountAsync(input.SectionId, input.PageId, input.CreatorId, EntryAuditStatus.Allowed, true);
            if (count == 0)
            {
                return new PagedResultDto<EntryDto>(0, new List<EntryDto>());
            }
            var result = await _entryRepository.GetListAsync(
                    input.SectionId,
                    input.PageId,
                    input.CreatorId,
                    EntryAuditStatus.Allowed,
                    true,
                    input.MaxResultCount,
                    input.SkipCount
                    );

            var dto = ObjectMapper.Map<List<Entry>, List<EntryDto>>(result);

            foreach (var entry in dto)
            {
                entry.SetDefaultsForCustomizeFields(
                    section.FieldDefinitions
                    .Select(fd => new BasicCustomizeFieldDefinition(
                        fd.Name,
                        fd.DisplayName,
                        fd.FieldControlProviderName,
                        fd.DefaultValue,
                        fd.Configuration
                        )).ToList()
                );

                var firstEntryOfSameEditor = dto.FirstOrDefault(e => e.Editor != null && e.CreatorId == entry.CreatorId);
                if (firstEntryOfSameEditor != null)
                    entry.Editor = firstEntryOfSameEditor.Editor;
                else
                    await FillEntryEditor(entry);
            }

            return new PagedResultDto<EntryDto>(count, dto);
        }


        private async Task<EntryDto> GetEntryDto(Entry entry)
        {
            if (entry == null)
            {
                return null;
            }

            if (entry.AuditStatus != EntryAuditStatus.Allowed)
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException("条目尚未通过审核");
            }

            if (!entry.IsActive)
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException("条目未启用");
            }

            var page= await _pageAppService.GetAsync(entry.PageId); //获取页面时，内部有验证授权的功能
            var section = await _sectionRepository.GetAsync(entry.SectionId);

            var dto = ObjectMapper.Map<Entry, EntryDto>(entry);
            dto.Page = page;
            dto.SetDefaultsForCustomizeFields(
                section.FieldDefinitions
                .Select(fd => new BasicCustomizeFieldDefinition(
                    fd.Name,
                    fd.DisplayName,
                    fd.FieldControlProviderName,
                    fd.DefaultValue,
                    fd.Configuration
                    )).ToList()
            );
            await FillEntryEditor(dto);

            return dto;
        }

        private async Task FillEntryEditor(EntryDto entry)
        {
            var editorUser = await UserLookupService.FindByIdAsync(entry.CreatorId.Value);
            if (editorUser != null)
            {
                entry.Editor = ObjectMapper.Map<SiteUser, SiteUserDto>(editorUser);
            }
        }
    }
}
