using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Cms.Sections;
using Dignite.Cms.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Sections
{
    public class SectionAppService : CmsAdminAppServiceBase, ISectionAppService
    {
        protected ISiteUserLookupService UserLookupService { get; }
        private readonly ISectionRepository _sectionRepository;
        private readonly ISectionGrantRepository _sectionAuthorizerRepository;
        private readonly IEnumerable<IFieldProvider> _fieldProviders;

        public SectionAppService(
            ISiteUserLookupService userLookupService,
            ISectionRepository sectionRepository,
            ISectionGrantRepository sectionAuthorizerRepository,
            IEnumerable<IFieldProvider> fieldProviders)
        {
            UserLookupService = userLookupService;
            _sectionRepository = sectionRepository;
            _sectionAuthorizerRepository = sectionAuthorizerRepository;
            _fieldProviders = fieldProviders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Section.Create)]
        public Task<NewSectionOutput> NewAsync()
        {
            var output = new NewSectionOutput();

            output.Section = new SectionCreateDto() {
                IsActive = true,
            };

            output.AllFieldProviders = _fieldProviders.Select(p => 
                new FieldProviderDto(
                    p.Name,
                    p.DisplayName,
                    p.ControlType
                    )
                ).ToImmutableList();

            return Task.FromResult(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Section.Update)]
        public async Task<EditSectionOutput> EditAsync(Guid id)
        {
            var section = await _sectionRepository.GetAsync(id,true);
            var output = new EditSectionOutput();


            output.Section = ObjectMapper.Map<Section, SectionUpdateDto>(section);
            output.AllFieldProviders = _fieldProviders.Select(p =>
                new FieldProviderDto(
                    p.Name,
                    p.DisplayName,
                    p.ControlType
                    )
                ).ToImmutableList();

            return output;
        }

        /// <summary>
        /// 创建新条目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [Authorize(Permissions.CmsPermissions.Section.Create)]
        public async Task<SectionDto> CreateAsync(SectionCreateDto input)
        {
            await CheckNameExistenceAsync(input.Name);

            var tenantId = CurrentTenant.Id;

            //
            var section = new Section(GuidGenerator.Create(),input.DisplayName, input.Name,input.TemplateFile,input.EntryTemplateFile, tenantId);
            

            //
            for(int i=0;i<input.FieldDefinitions.Count;i++)
            {
                var fd = input.FieldDefinitions.ElementAt(i);
                section.AddFieldDefinition(
                    new FieldDefinition(fd.Id,
                        section.Id, 
                        fd.DisplayName,
                        fd.Name,
                        fd.DefaultValue, 
                        fd.FieldProviderName, 
                        fd.Configuration,
                        i, 
                        tenantId)
                    );
            }

            //
            await _sectionRepository.InsertAsync(section);
            return ObjectMapper.Map<Section, SectionDto>(section);
        }

        /// <summary>
        /// 更新条目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Section.Update)]
        public async Task<SectionDto> UpdateAsync(Guid id, SectionUpdateDto input)
        {
            var section = await _sectionRepository.GetAsync(id);
            if (!section.Name.Equals(input.Name,StringComparison.OrdinalIgnoreCase))
            {
                await CheckNameExistenceAsync(input.Name);
            }

            var tenantId = CurrentTenant.Id;


            //更新条目类型
            section.IsActive = input.IsActive;
            section.DisplayName = input.DisplayName;
            section.Name = input.Name;
            section.TemplateFile = input.TemplateFile;
            section.EntryTemplateFile = input.EntryTemplateFile;


            //更新字段
            for (int i = 0; i < input.FieldDefinitions.Count; i++)
            {
                var fd = input.FieldDefinitions.ElementAt(i);
                if (section.FieldDefinitions.Any(m => m.Id == fd.Id))
                {
                    section.UpdateFieldDefinition(fd.Id,fd.DisplayName,fd.Name,fd.DefaultValue, fd.Configuration,i);
                }
                else
                {
                    section.AddFieldDefinition(new FieldDefinition(
                        fd.Id, 
                        section.Id, 
                        fd.DisplayName, 
                        fd.Name, 
                        fd.DefaultValue, 
                        fd.FieldProviderName, 
                        fd.Configuration,
                        i, 
                        tenantId)
                    );
                }
            }
            foreach (var fd in section.FieldDefinitions)
            {
                if (!input.FieldDefinitions.Any(m => m.Id == fd.Id))
                {
                    section.DeleteFieldDefinition(fd.Id);
                }
            }

            //
            await _sectionRepository.UpdateAsync(section);
            return ObjectMapper.Map<Section, SectionDto>(section);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Section.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _sectionRepository.DeleteAsync(id);
        }

        [Authorize(Permissions.CmsPermissions.Section.Default)]
        public async Task<PagedResultDto<SectionDto>> GetListAsync(GetSectionsInput input)
        {
            var authorizerId = (await AuthorizationService.IsGrantedAsync(Permissions.CmsPermissions.Page.SuperAuthorization)) ? null : CurrentUser.Id;
            var count = await _sectionRepository.GetCountAsync(input.Filter, authorizerId);
            var sections = await _sectionRepository.GetListAsync(input.Filter, authorizerId,input.MaxResultCount, input.SkipCount);

            var result = ObjectMapper.Map<List<Section>, List<SectionDto>>(sections);

            return new PagedResultDto<SectionDto>(count,result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsPermissions.Section.Default)]
        public async Task<SectionDto> GetAsync(Guid id)
        {
            var result = await _sectionRepository.GetAsync(id, true);
            return ObjectMapper.Map<Section, SectionDto>(
                result
                );
        }

        [Authorize(Permissions.CmsPermissions.Section.Update)]
        public async Task AddAuthorizerAsync(Guid id, AuthorizerEditInput input)
        { 
            var authorizer = await _sectionAuthorizerRepository.GetAsync(id,input.UserId);
            if (authorizer != null)
            {
                authorizer.PageIds = input.PageIds;
                await _sectionAuthorizerRepository.UpdateAsync(authorizer);
            }
            else
            {
                await _sectionAuthorizerRepository.InsertAsync(authorizer);
            }
        }

        [Authorize(Permissions.CmsPermissions.Section.Update)]
        public async Task UpdateAuthorizerAsync(Guid id, AuthorizerEditInput input)
        {
            var authorizer = await _sectionAuthorizerRepository.GetAsync(id, input.UserId);
            if (authorizer != null)
            {
                authorizer.PageIds=input.PageIds;
                await _sectionAuthorizerRepository.UpdateAsync(authorizer);
            }
        }

        [Authorize(Permissions.CmsPermissions.Section.Update)]
        public async Task RemoveAuthorizerAsync(Guid id, Guid userId)
        {
            var authorizer = await _sectionAuthorizerRepository.GetAsync(id, userId);
            if (authorizer != null)
            {
                await _sectionAuthorizerRepository.DeleteAsync(authorizer);
            }
        }

        [Authorize(Permissions.CmsPermissions.Section.Update)]
        public async Task<ListResultDto<SectionAuthorizerDto>> GetAuthorizersAsync(Guid id)
        {
            var result = await _sectionAuthorizerRepository.GetListAsync(id);
            var dto = ObjectMapper.Map<List<SectionGrant>, List<SectionAuthorizerDto>>(
                result
                );


            foreach (var authorizer in dto)
            {
                var authorizerUser = await UserLookupService.FindByIdAsync(authorizer.UserId);
                if (authorizerUser != null)
                {
                    authorizer.User = ObjectMapper.Map<SiteUser, SiteUserDto>(authorizerUser);
                }
            }

            return new ListResultDto<SectionAuthorizerDto>(dto);
        }


        protected virtual async Task CheckNameExistenceAsync( string name)
        {
            if (await _sectionRepository.NameExistsAsync(name))
            {
                throw new SectionNameAlreadyExistException( name);
            }
        }
    }
}
