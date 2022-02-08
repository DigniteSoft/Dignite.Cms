using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;
using Dignite.SiteBuilding.Sections;
using Dignite.SiteBuilding.Entries;

namespace Dignite.SiteBuilding.Admin.Entries
{
    public class EntryAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Entry>
    {
        private readonly IPermissionChecker _permissionChecker;
        private readonly ISectionRepository _sectionRepository;

        public EntryAuthorizationHandler(IPermissionChecker permissionChecker, ISectionRepository sectionRepository)
        {
            _permissionChecker = permissionChecker;
            _sectionRepository = sectionRepository;
        }

        protected async override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Entry resource)
        {
            if (requirement.Name == CommonOperations.Delete.Name && await HasDeletePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == CommonOperations.Update.Name && await HasUpdatePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == CommonOperations.Create.Name && await HasCreatePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }
        }

        private async Task<bool> HasDeletePermission(AuthorizationHandlerContext context, Entry resource)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, Permissions.SiteBuildingPermissions.Entry.Delete))
            {
                if (await _permissionChecker.IsGrantedAsync(context.User, Permissions.SiteBuildingPermissions.Page.SuperAuthorization))
                    return true;
                else
                {
                    if (resource.CreatorId != null 
                        && resource.CreatorId == context.User.FindUserId() 
                        && await IsAuthorizedAsync(context,resource)
                        )
                        return true;
                }
            }

            return false;
        }

        private async Task<bool> HasUpdatePermission(AuthorizationHandlerContext context, Entry resource)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, Permissions.SiteBuildingPermissions.Entry.Update))
            {
                if (await _permissionChecker.IsGrantedAsync(context.User, Permissions.SiteBuildingPermissions.Page.SuperAuthorization))
                    return true;
                else
                {
                    if (resource.CreatorId != null
                        && resource.CreatorId == context.User.FindUserId()
                        && await IsAuthorizedAsync(context, resource)
                        )
                        return true;
                }
            }

            return false;
        }

        private async Task<bool> HasCreatePermission(AuthorizationHandlerContext context, Entry resource)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, Permissions.SiteBuildingPermissions.Entry.Create))
            {
                if (await _permissionChecker.IsGrantedAsync(context.User, Permissions.SiteBuildingPermissions.Page.SuperAuthorization))
                    return true;
                else
                    return await IsAuthorizedAsync(context, resource);                
            }

            return false;
        }

        private async Task<bool> IsAuthorizedAsync(AuthorizationHandlerContext context, Entry resource)
        {
            return await _sectionRepository.IsAuthorizedAsync(resource.SectionId, resource.PageId, context.User.FindUserId().Value);
        }
    }
}