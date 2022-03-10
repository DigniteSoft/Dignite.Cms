
using Dignite.SiteBuilding.Sections;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.SiteBuilding.Admin.Sections
{
    public interface ISectionAppService
    : ICrudAppService<
        SectionDto,
        Guid,
        GetSectionsInput,
        SectionCreateDto,
        SectionUpdateDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<NewSectionOutput> NewAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<EditSectionOutput> EditAsync(Guid id);


        /// <summary>
        /// 授权人员管理条目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddAuthorizerAsync(Guid id, AuthorizerEditInput input);

        /// <summary>
        /// 更新授权人员管理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAuthorizerAsync(Guid id, AuthorizerEditInput input);

        /// <summary>
        /// 移除授权管理人员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RemoveAuthorizerAsync(Guid id, Guid userId);

        /// <summary>
        /// 获取授权的管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ListResultDto<SectionAuthorizerDto>> GetAuthorizersAsync(Guid id);
    }
}
