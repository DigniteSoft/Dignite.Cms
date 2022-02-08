using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;

namespace Dignite.SiteBuilding.Pages
{
    public class PageAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Page>
    {
        private readonly IPermissionChecker _permissionChecker;

        public PageAuthorizationHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected async override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Page resource)
        {
            if (requirement.Name == CommonOperations.Read.Name && await HasReadPermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }
        }

        private async Task<bool> HasReadPermission(AuthorizationHandlerContext context, Page resource)
        {
            if (resource.PermissionName.IsNullOrEmpty())
                return true;
            else
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    return false;
                }
                else{
                    if (await _permissionChecker.IsGrantedAsync(context.User, Permissions.SiteBuildingPermissions.Page.SuperAuthorization))
                        return true;
                    else
                        return await _permissionChecker.IsGrantedAsync(context.User, resource.PermissionName);
                }
            }
        }
    }
}