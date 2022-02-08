using Dignite.Abp.FieldCustomizing.FieldControls;
using Dignite.SiteBuilding.Sections;
using Dignite.SiteBuilding.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Admin.Sections
{
    public class SectionAppService : SiteBuildingAdminAppServiceBase, ISectionAppService
    {
        protected ISiteUserLookupService UserLookupService { get; }
        private readonly ISectionRepository _sectionRepository;
        private readonly ISectionGrantRepository _sectionAuthorizerRepository;
        private readonly IEnumerable<IFieldControlProvider> _fieldControlProviders;

        public SectionAppService(
            ISiteUserLookupService userLookupService,
            ISectionRepository sectionRepository,
            ISectionGrantRepository sectionAuthorizerRepository,
            IEnumerable<IFieldControlProvider> fieldControlProviders)
        {
            UserLookupService = userLookupService;
            _sectionRepository = sectionRepository;
            _sectionAuthorizerRepository = sectionAuthorizerRepository;
            _fieldControlProviders = fieldControlProviders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Permissions.SiteBuildingPermissions.Section.Create)]
        public Task<SectionEditOutput> NewAsync()
        {
            var output = new SectionEditOutput();

            output.Section = new SectionEditDto();

            output.AllFieldProviders = _fieldControlProviders.Select(p => 
                new FieldControlProviderDto(
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
        [Authorize(Permissions.SiteBuildingPermissions.Section.Update)]
        public async Task<SectionEditOutput> EditAsync(Guid id)
        {
            var section = await _sectionRepository.GetAsync(id,true);
            var output = new SectionEditOutput();


            output.Section = ObjectMapper.Map<Section, SectionEditDto>(section);
            output.AllFieldProviders = _fieldControlProviders.Select(p =>
                new FieldControlProviderDto(
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

        [Authorize(Permissions.SiteBuildingPermissions.Section.Create)]
        public async Task CreateAsync(SectionEditDto input)
        {
            await CheckNameExistenceAsync(input.Name);

            var tenantId = CurrentTenant.Id;

            //
            var section = new Section(GuidGenerator.Create(),input.DisplayName, input.Name,input.TemplateFile,input.EntryTemplateFile, tenantId);
            

            //
            foreach (var fd in input.FieldDefinitions)
            {
                section.AddFieldDefinition(new FieldDefinition(fd.Id, fd.DisplayName,fd.Name,fd.DefaultValue, fd.Configuration, tenantId)
                {
                    SectionId=section.Id,
                    Description = fd.Description,
                    Position = fd.Position
                });
            }

            //
            await _sectionRepository.InsertAsync(section);
        }

        /// <summary>
        /// 更新条目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.SiteBuildingPermissions.Section.Update)]
        public async Task UpdateAsync(Guid id, SectionEditDto input)
        {
            var section = await _sectionRepository.GetAsync(id);
            if (!section.Name.Equals(input.Name,StringComparison.OrdinalIgnoreCase))
            {
                await CheckNameExistenceAsync(input.Name);
            }

            var tenantId = CurrentTenant.Id;


            //更新条目类型
            section.DisplayName = input.DisplayName;
            section.Name = input.Name;
            section.TemplateFile = input.TemplateFile;
            section.EntryTemplateFile = input.EntryTemplateFile;


            //更新字段
            foreach (var fd in input.FieldDefinitions)
            {
                if (section.FieldDefinitions.Any(m => m.Id == fd.Id))
                {
                    section.UpdateFieldDefinition(fd.Id,fd.DisplayName,fd.Name,fd.DefaultValue,fd.Description, fd.Configuration,fd.Position);
                }
                else
                {
                    section.AddFieldDefinition(new FieldDefinition(fd.Id,  fd.DisplayName, fd.Name, fd.DefaultValue, fd.Configuration, tenantId)
                    {
                        SectionId = section.Id,
                        Description = fd.Description,
                        Position = fd.Position
                    });
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.SiteBuildingPermissions.Section.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _sectionRepository.DeleteAsync(id);
        }

        [Authorize(Permissions.SiteBuildingPermissions.Section.Default)]
        public async Task<PagedResultDto<SectionDto>> GetListAsync(GetSectionsInput input)
        {
            var authorizerId = (await AuthorizationService.IsGrantedAsync(Permissions.SiteBuildingPermissions.Page.SuperAuthorization)) ? null : CurrentUser.Id;
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
        [Authorize(Permissions.SiteBuildingPermissions.Section.Default)]
        public async Task<SectionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Section, SectionDto>(
                await _sectionRepository.GetAsync(id, true)
                );
        }

        [Authorize(Permissions.SiteBuildingPermissions.Section.Update)]
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

        [Authorize(Permissions.SiteBuildingPermissions.Section.Update)]
        public async Task UpdateAuthorizerAsync(Guid id, AuthorizerEditInput input)
        {
            var authorizer = await _sectionAuthorizerRepository.GetAsync(id, input.UserId);
            if (authorizer != null)
            {
                authorizer.PageIds=input.PageIds;
                await _sectionAuthorizerRepository.UpdateAsync(authorizer);
            }
        }

        [Authorize(Permissions.SiteBuildingPermissions.Section.Update)]
        public async Task RemoveAuthorizerAsync(Guid id, Guid userId)
        {
            var authorizer = await _sectionAuthorizerRepository.GetAsync(id, userId);
            if (authorizer != null)
            {
                await _sectionAuthorizerRepository.DeleteAsync(authorizer);
            }
        }

        [Authorize(Permissions.SiteBuildingPermissions.Section.Update)]
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
