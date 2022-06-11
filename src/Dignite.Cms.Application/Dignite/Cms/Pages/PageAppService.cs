using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace Dignite.Cms.Pages
{
    public class PageAppService : CmsAppService, IPageAppService
    {
        private readonly IPageRepository _pageRepository;

        public PageAppService(IPageRepository siteRepository)
        {
            _pageRepository = siteRepository;
        }
        public async Task<PageDto> GetAsync(Guid id)
        {
            var result = await _pageRepository.GetAsync(id);
            await AuthorizationService.CheckAsync(result, CommonOperations.Read);
            return ObjectMapper.Map<Page, PageDto>(result);
        }

        public async Task<PageDto> FindByPathAsync(string path)
        {
            var result = await _pageRepository.FindByPathAsync(path);
            if (result == null)
                return null;
            else
            {
                await AuthorizationService.CheckAsync(result, CommonOperations.Read);
                return ObjectMapper.Map<Page, PageDto>(result);
            }
        }

        public async Task<ListResultDto<PageDto>> GetListAsync()
        {
            var result = await _pageRepository.GetListAsync();
            var list=new List<PageDto>();
            foreach (var p in result)
            {
                if (!p.IsActive)
                    continue;

                //检查权限
                if (!p.PermissionName.IsNullOrEmpty())
                {
                    if (!CurrentUser.IsAuthenticated)
                    {
                        continue;
                    }
                    else
                    {
                        if (!await AuthorizationService.IsGrantedAsync(Permissions.CmsPermissions.Page.SuperAuthorization))
                        {
                            if(!await AuthorizationService.IsGrantedAsync(p.PermissionName))
                                continue;
                        }
                    }
                }
                list.Add(ObjectMapper.Map<Page, PageDto>(p));
            }

            //重构成树开数据结构
            var dto = new List<PageDto>();
            dto.AddRange(list.Where(p => !p.ParentId.HasValue).ToList());
            foreach (var page in dto)
            {
                AddChildren(page, list);
            }

            return new ListResultDto<PageDto>(dto);
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