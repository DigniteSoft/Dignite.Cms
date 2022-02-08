using Dignite.SiteBuilding.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.SiteBuilding.Permissions
{
    public class SiteBuildingPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(SiteBuildingPermissions.GroupName, L("Permission:SiteBuilding"));

            var pages = group.AddPermission(SiteBuildingPermissions.Page.Default, L("Permission:Pages"));
            pages.AddChild(SiteBuildingPermissions.Page.Create, L("Permission:Create"));
            pages.AddChild(SiteBuildingPermissions.Page.Update, L("Permission:Edit"));
            pages.AddChild(SiteBuildingPermissions.Page.Delete, L("Permission:Delete"));
            pages.AddChild(SiteBuildingPermissions.Page.SuperAuthorization, L("Permission:SuperAuthorization"));

            var sections = group.AddPermission(SiteBuildingPermissions.Section.Default, L("Permission:Section"));
            sections.AddChild(SiteBuildingPermissions.Section.Create, L("Permission:Create"));
            sections.AddChild(SiteBuildingPermissions.Section.Update, L("Permission:Edit"));
            sections.AddChild(SiteBuildingPermissions.Section.Delete, L("Permission:Delete"));

            var entries = group.AddPermission(SiteBuildingPermissions.Entry.Default, L("Permission:Entry"));
            entries.AddChild(SiteBuildingPermissions.Entry.Create, L("Permission:Create"));
            entries.AddChild(SiteBuildingPermissions.Entry.Update, L("Permission:Edit"));
            entries.AddChild(SiteBuildingPermissions.Entry.Delete, L("Permission:Delete"));
            entries.AddChild(SiteBuildingPermissions.Entry.Audit, L("Permission:Audit"));

            var users = group.AddPermission(SiteBuildingPermissions.User.Default, L("Permission:User"));
            users.AddChild(SiteBuildingPermissions.User.Create, L("Permission:Create"));
            users.AddChild(SiteBuildingPermissions.User.Update, L("Permission:Edit"));
            users.AddChild(SiteBuildingPermissions.User.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SiteBuildingResource>(name);
        }
    }
}