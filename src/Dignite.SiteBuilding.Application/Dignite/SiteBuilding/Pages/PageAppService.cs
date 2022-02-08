using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Dignite.SiteBuilding.Pages
{
    public class PageAppService : SiteBuildingAppService, IPageAppService
    {
        private readonly IPageRepository _siteRepository;

        public PageAppService(IPageRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }
        public async Task<PageDto> GetAsync(Guid id)
        {
            var result = await _siteRepository.GetAsync(id);
            await AuthorizationService.CheckAsync(result, CommonOperations.Read);
            return ObjectMapper.Map<Page, PageDto>(result);
        }

        public async Task<PageDto> FindByPathAsync(string path)
        {
            var result = await _siteRepository.FindByPathAsync(path);
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
            var result = await _siteRepository.GetListAsync();
            var dto=new List<PageDto>();
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
                        if (!await AuthorizationService.IsGrantedAsync(Permissions.SiteBuildingPermissions.Page.SuperAuthorization))
                        {
                            if(!await AuthorizationService.IsGrantedAsync(p.PermissionName))
                                continue;
                        }
                    }
                }

                dto.Add(ObjectMapper.Map<Page, PageDto>(p));
            }

            return new ListResultDto<PageDto>(dto);
        }
    }
}