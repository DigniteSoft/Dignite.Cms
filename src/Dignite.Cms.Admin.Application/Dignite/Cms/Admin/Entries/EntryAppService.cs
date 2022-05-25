using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.FieldCustomizing.FieldControls.EntryChoice;
using Dignite.Cms.Entries;
using Dignite.Cms.Sections;
using Dignite.Cms.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Entries
{
    public class EntryAppService : CmsAdminAppServiceBase, IEntryAppService
    {
        protected ISiteUserLookupService UserLookupService { get; }
        private readonly IEntryRepository _entryRepository;        
        private readonly ISectionRepository _sectionRepository;
        private readonly IFieldDefinitionRepository _fieldDefinitionRepository;
        private readonly ISectionGrantRepository _sectionAuthorizerRepository;

        public EntryAppService(
            ISiteUserLookupService userLookupService,
            IEntryRepository entryRepository, 
            ISectionRepository sectionRepository,
            IFieldDefinitionRepository fieldDefinitionRepository,
            ISectionGrantRepository sectionAuthorizerRepository)
        {
            UserLookupService = userLookupService;
            _entryRepository = entryRepository;
            _sectionRepository = sectionRepository;
            _fieldDefinitionRepository= fieldDefinitionRepository;
            _sectionAuthorizerRepository = sectionAuthorizerRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Create)]
        public async Task<NewEntryOutput> NewAsync(Guid sectionId)
        {
            var section = await _sectionRepository.GetAsync(sectionId,true);
            var output = new NewEntryOutput();
            output.Section = ObjectMapper.Map<Section,SectionDto>(section);
            output.Entry = new EntryCreateDto( sectionId);
            output.Entry.SetDefaultsForCustomizeFields(
                section.FieldDefinitions
                .Select(fd => new BasicCustomizeFieldDefinition(
                        fd.Name,
                        fd.DisplayName,
                        fd.FieldControlProviderName,
                        fd.DefaultValue,
                        fd.Configuration
                        )).ToList()
                );

            return output;
        }

        [Authorize(Permissions.CmsPermissions.Entry.Update)]
        public async Task<EditEntryOutput> EditAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id, true);

            await AuthorizationService.CheckAsync(entry, CommonOperations.Update);

            var section = await _sectionRepository.GetAsync(entry.SectionId,true);
            var output = new EditEntryOutput();
            output.Section = ObjectMapper.Map<Section, SectionDto>(section);
            output.Entry = ObjectMapper.Map<Entry, EntryUpdateDto>(entry);


            output.Entry.SetDefaultsForCustomizeFields(
                section.FieldDefinitions
                .Select(fd => new BasicCustomizeFieldDefinition(
                        fd.Name,
                        fd.DisplayName,
                        fd.FieldControlProviderName,
                        fd.DefaultValue,
                        fd.Configuration
                        )).ToList()
            );

            return output;
        }

        /// <summary>
        /// 创建或更新条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [Authorize(Permissions.CmsPermissions.Entry.Create)]
        public async Task<EntryDto> CreateAsync(EntryCreateDto input)
        {
            if(!input.Slug.IsNullOrWhiteSpace())
            await CheckSlugExistenceAsync(input.PageId,input.Slug);

            var id = GuidGenerator.Create();
            var entry = new Entry(id, input.SectionId, input.PageId,input.IsActive,input.Slug, input.PublishTime, CurrentTenant.Id);
            await AuthorizationService.CheckAsync(entry, CommonOperations.Create);
            //
            if (await AuthorizationService.IsGrantedAsync(Permissions.CmsPermissions.Entry.Audit))
            {
                entry.SetAuditStatus(EntryAuditStatus.Allowed);
            }

            //
            entry.CustomizedFields = input.CustomizedFields;
            //entry.SetPosition((await _entryRepository.GetMaxPositionAsync(input.SectionId, input.PageId))+1);            

            await _entryRepository.InsertAsync(entry);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Update)]
        public async Task<EntryDto> UpdateAsync(Guid id, EntryUpdateDto input)
        {
            var entry = await _entryRepository.GetAsync(id, true);

            if (!input.Slug.IsNullOrWhiteSpace()
                && (input.PageId!=entry.PageId 
                    || !input.Slug.Equals(entry.Slug,StringComparison.OrdinalIgnoreCase)
                    )
                )
                await CheckSlugExistenceAsync( input.PageId, input.Slug);


            await AuthorizationService.CheckAsync(entry, CommonOperations.Update);
            //
            if (await AuthorizationService.IsGrantedAsync(Permissions.CmsPermissions.Entry.Audit))
            {
                entry.SetAuditStatus(EntryAuditStatus.Allowed);
            }

            //
            entry.PageId= input.PageId;
            entry.Slug = input.Slug;
            entry.PublishTime = input.PublishTime;
            entry.IsActive = input.IsActive;
            entry.CustomizedFields = input.CustomizedFields;


            await _entryRepository.UpdateAsync(entry);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id, false);

            await AuthorizationService.CheckAsync(entry, CommonOperations.Delete);
            await _entryRepository.DeleteAsync(entry);

            //TODO:考虑同步删除文件类型字段中的blob
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Default)]
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            var section = await _sectionRepository.GetAsync(input.SectionId, true);

            if (!await AuthorizationService.IsGrantedAsync(Permissions.CmsPermissions.Page.SuperAuthorization))
            {
                if (input.PageId.HasValue
                    &&
                    !(await _sectionRepository.IsAuthorizedAsync(section.Id, input.PageId.Value, CurrentUser.Id.Value))
                    )
                {
                    //TODO
                    throw new Exception($"无权获取{input.PageId.Value}下的条目列表");
                }
            }

            if (!await AuthorizationService.IsGrantedAsync(Permissions.CmsPermissions.Entry.Audit))
            {
                input.CreatorId = CurrentUser.Id.Value;
            }

            return await GetListAsync(section,input.PageId,input.CreatorId,input.AuditedStatus,input.IsActive,input.MaxResultCount,input.SkipCount);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Default)]
        public async Task<EntryDto> GetAsync(Guid id)
        {
            var result = await _entryRepository.GetAsync(id, true);
            return ObjectMapper.Map<Entry, EntryDto>(
                result
                );
        }


        /// <summary>
        /// 获取条目选择字段的数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Default)]
        public async Task<PagedResultDto<EntryDto>> GetFieldSourceAsync(GetEntryChoiceFieldSourceInput input)
        {
            var field = await _fieldDefinitionRepository.GetAsync(input.FieldId, false);
            var fieldConfig = new EntryChoiceConfiguration(field.Configuration);

            if (!await AuthorizationService.IsGrantedAsync(Permissions.CmsPermissions.Page.SuperAuthorization))
            {
                var authorizers = await _sectionAuthorizerRepository.GetListAsync(fieldConfig.SectionId);
                if (!authorizers.Any(auth => auth.UserId == CurrentUser.Id.Value))
                {
                    //TODO
                    throw new Volo.Abp.UserFriendlyException($"无权访问 {field.DisplayName}字段的数据源！");
                }
            }

            return await GetListAsync(
                await _sectionRepository.GetAsync(fieldConfig.SectionId),
                fieldConfig.PageId, null, EntryAuditStatus.Allowed, true, input.MaxResultCount, input.SkipCount);
        }


        /// <summary>
        /// 移动条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Update)]
        public async Task MoveAsync(MoveEntriesInput input)
        {
            var section = await _sectionRepository.GetAsync(input.SectionId, false);
            var entries = await _entryRepository.GetListAsync(input.SectionId, input.Ids);
            var beforEntry = input.BeforId.HasValue ? await _entryRepository.GetAsync(input.BeforId.Value) : null;
            int newPosition = 1;

            if(beforEntry!=null && beforEntry.SectionId!=input.SectionId)
                //TODO
                throw new Volo.Abp.UserFriendlyException($"{beforEntry}的条目类型不是{section.DisplayName}类型，无法移动！");

            if (beforEntry != null)
            {
                newPosition = beforEntry.Position + 1;
            }
            else if(input.TargetPageId.HasValue)
            {
                newPosition = (await _entryRepository.GetMaxPositionAsync(input.SectionId, input.TargetPageId.Value))+1;
            }

            for(var i=0;i<entries.Count;i++)
            {
                var entry = entries[i];
                await AuthorizationService.CheckAsync(entry, CommonOperations.Update);
                if (input.TargetPageId.HasValue && input.TargetPageId.Value!=entry.PageId)
                {
                    entry.PageId = input.TargetPageId.Value;
                    await AuthorizationService.CheckAsync(entry, CommonOperations.Update); //再次验证移动后的授权
                }
                entry.SetPosition(newPosition+i);
                await _entryRepository.UpdateAsync(entry);
            }
        }


        /// <summary>
        /// 审核内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAllow"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Entry.Audit)]
        public async Task AuditAsync(AuditEntriesInput input)
        {
            var entries = await _entryRepository.GetListAsync(input.SectionId, input.Ids);

            foreach(var entry in entries)
            {
                await AuthorizationService.CheckAsync(entry, CommonOperations.Update);
                entry.SetAuditStatus(input.IsAllowed ? EntryAuditStatus.Allowed : EntryAuditStatus.Disallowed);

                //TODO:添加审核通知功能
            }
        }


        protected virtual async Task CheckSlugExistenceAsync(Guid pageId, string slug)
        {
            if (await _entryRepository.AnyAsync(pageId,slug))
            {
                throw new EntrySlugAlreadyExistException( pageId, slug);
            }
        }

        protected virtual async Task<PagedResultDto<EntryDto>> GetListAsync(Section section,Guid? pageId=null,Guid? creatorId=null, EntryAuditStatus? auditedStatus=null,bool? isActive=null,int maxResultCount=50,int skipCount=0)
        {
            var count = await _entryRepository.GetCountAsync(section.Id, pageId, creatorId, auditedStatus, isActive);
            if (count == 0)
                return new PagedResultDto<EntryDto>(0, new List<EntryDto>());

            //get entry list
            var result = ObjectMapper.Map<List<Entry>, List<EntryDto>>(
                await _entryRepository.GetListAsync(section.Id, pageId, creatorId, auditedStatus, isActive,maxResultCount, skipCount)
                    );

            foreach (var entry in result)
            {
                var editorUser = await UserLookupService.FindByIdAsync(entry.CreatorId.Value);
                if (editorUser != null)
                {
                    entry.Editor = ObjectMapper.Map<SiteUser, SiteUserDto>(editorUser);
                }

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
            }

            return new PagedResultDto<EntryDto>(count, result);
        }
    }
}
