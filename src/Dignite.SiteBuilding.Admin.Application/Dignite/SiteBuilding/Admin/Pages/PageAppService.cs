using Dignite.SiteBuilding.Pages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

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
        public async Task<ListResultDto<PageDto>> GetListAsync()
        {
            var result = await _pageRepository.GetListAsync();

            return new ListResultDto<PageDto>( 
                ObjectMapper.Map<List<Page>, List<PageDto>>(
                    result
                    ));
        }

        [Authorize(Permissions.SiteBuildingPermissions.Page.Update)]
        public async Task<PageEditOutput> EditAsync(Guid id)
        {
            var result = await _pageRepository.GetAsync(id);

            return ObjectMapper.Map<Page, PageEditOutput>(result);
        }

        [Authorize(Permissions.SiteBuildingPermissions.Page.Create)]
        public async Task CreateAsync(PageEditDto edit)
        {
            var path = await GetPath(edit.ParentId, edit.Name);
            await CheckPathExistenceAsync(path);

            var page = new Page(
                GuidGenerator.Create(),
                edit.ParentId,
                edit.IsActive,
                edit.Title,
                path,
                CurrentTenant.Id);
            page.Description = edit.Description;
            page.Keywords = edit.Keywords;
            page.PermissionName= edit.PermissionName;
            page.TemplateFile=edit.TemplateFile;


            page.SetPosition(
                (await _pageRepository.GetListAsync(edit.ParentId))
                .Select(x => x.Position)
                .DefaultIfEmpty(0)
                .Max() +1);

            await _pageRepository.InsertAsync(page);
        }

        [Authorize(Permissions.SiteBuildingPermissions.Page.Update)]
        public async Task UpdateAsync(Guid id, PageEditDto edit)
        {
            var path = await GetPath(edit.ParentId, edit.Name);
            var page = await _pageRepository.GetAsync(id);
            if(page.ParentId!=edit.ParentId || !page.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
                await CheckPathExistenceAsync(path);


            page.IsActive = edit.IsActive;
            page.Title = edit.Title;
            page.Path = path;
            page.Description = edit.Description;
            page.ParentId = edit.ParentId;
            page.Description = edit.Description;
            page.Keywords = edit.Keywords;
            page.PermissionName = edit.PermissionName;
            page.TemplateFile = edit.TemplateFile;

            await _pageRepository.UpdateAsync(page);
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
    }
}