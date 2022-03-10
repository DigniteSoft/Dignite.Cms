using Dignite.SiteBuilding.Pages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Dignite.SiteBuilding.Admin.Pages
{
    public class PageAppService : SiteBuildingAdminAppServiceBase, IPageAppService
    {
        private readonly IPageRepository _pageRepository;

        public PageAppService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }


        [Authorize(Permissions.SiteBuildingPermissions.Page.Default)]
        public virtual async Task<PageDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Page, PageDto>(
                await _pageRepository.GetAsync(id)
            );
        }

        [Authorize(Permissions.SiteBuildingPermissions.Page.Default)]
        public virtual async Task<ListResultDto<PageDto>> GetAllListAsync()
        {
            var result = await _pageRepository.GetListAsync();

            var list = new List<PageDto>(
                ObjectMapper.Map<List<Page>, List<PageDto>>(
                    result
                    ));

            var dto = new List<PageDto>();
            dto.AddRange(list.Where(p => !p.ParentId.HasValue).ToList());
            foreach (var page in dto)
            {
                AddChildren(page, list);
            }

            return new ListResultDto<PageDto>(dto);
        }


        [Authorize(Permissions.SiteBuildingPermissions.Page.Default)]
        public virtual async Task<PagedResultDto<PageDto>> GetListAsync(GetPagesInput input)
        {
            var result = await _pageRepository.GetListAsync(input.ParentId);

            var dto = 
                ObjectMapper.Map<List<Page>, List<PageDto>>(
                    result
                    );

            return new PagedResultDto<PageDto>(result.Count, dto);
        }


        [Authorize(Permissions.SiteBuildingPermissions.Page.Create)]
        public async Task<PageDto> CreateAsync(PageCreateDto input)
        {
                var path = await GetPath(input.ParentId, input.Name);
                await CheckPathExistenceAsync(path);

                var page = new Page(
                    GuidGenerator.Create(),
                    input.ParentId,
                    input.IsActive,
                    input.Title,
                    path,
                    CurrentTenant.Id);
                page.Description = input.Description;
                page.Keywords = input.Keywords;
                page.PermissionName = input.PermissionName;
                page.TemplateFile = input.TemplateFile;


                page.SetPosition(
                    (await _pageRepository.GetListAsync(input.ParentId))
                    .Select(x => x.Position)
                    .DefaultIfEmpty(0)
                    .Max() + 1);

                await _pageRepository.InsertAsync(page);

                return ObjectMapper.Map<Page, PageDto>(page);
        }

        [Authorize(Permissions.SiteBuildingPermissions.Page.Update)]
        public async Task<PageDto> UpdateAsync(Guid id, PageUpdateDto input)
        {
            var page = await _pageRepository.GetAsync(id);

            page.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            var path = await GetPath(input.ParentId, input.Name);
            if (page.ParentId!=input.ParentId || !page.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
                await CheckPathExistenceAsync(path);


            page.IsActive = input.IsActive;
            page.Title = input.Title;
            page.Path = path;
            page.Description = input.Description;
            page.ParentId = input.ParentId;
            page.Description = input.Description;
            page.Keywords = input.Keywords;
            page.PermissionName = input.PermissionName;
            page.TemplateFile = input.TemplateFile;

            await _pageRepository.UpdateAsync(page);

            return ObjectMapper.Map<Page, PageDto>(page);
        }

        [Authorize(Permissions.SiteBuildingPermissions.Page.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _pageRepository.DeleteAsync(id);
        }

        [Authorize(Permissions.SiteBuildingPermissions.Page.Update)]
        public async Task MoveAsync(Guid id,MovePageInput input)
        {
            var page = await _pageRepository.GetAsync(id);
            var children = await _pageRepository.GetListAsync(input.ParentId);
            if (input.ParentId != page.ParentId)
            {
                var pageName = page.Path.Substring(page.Path.LastIndexOf('/') + 1);
                if (input.ParentId.HasValue)
                {
                    var parentPage = await _pageRepository.GetAsync(input.ParentId.Value);
                    page.Path = parentPage.Path.EnsureEndsWith('/') + pageName;
                }
                else
                {
                    page.Path = pageName;
                }
                page.ParentId = input.ParentId;
            }

            if (input.BeforId.HasValue)
            {
                var beforPage = children.FirstOrDefault(p => p.Id == input.BeforId.Value);
                foreach (var p in children.Where(p => p.Position > beforPage.Position))
                {
                    p.SetPosition(p.Position + 1);
                }
                page.SetPosition(beforPage.Position + 1);
            }
            else
            {
                foreach (var p in children)
                {
                    p.SetPosition(p.Position + 1);
                }
                page.SetPosition(1);
            }
        }


        protected virtual async Task CheckPathExistenceAsync( string path)
        {
            if (await _pageRepository.PathExistsAsync(path))
            {
                throw new PagePathAlreadyExistException(path);
            }
        }

        protected virtual async Task<string> GetPath(Guid? parentId, string name)
        {
            string path = name;
            if (parentId.HasValue)
            {
                var parent = await _pageRepository.GetAsync(parentId.Value);
                path = parent.Path.EnsureEndsWith('/') + name;
            }

            return path;
        }

        private void AddChildren(PageDto parent, List<PageDto> list)
        {
            var children = list.Where(p => p.ParentId == parent.Id).ToList();
            if (children.Any())
            { 
                parent.Children = children;

                foreach (var page in children)
                {
                    AddChildren(page, list);
                }
            }
        }
    }
}