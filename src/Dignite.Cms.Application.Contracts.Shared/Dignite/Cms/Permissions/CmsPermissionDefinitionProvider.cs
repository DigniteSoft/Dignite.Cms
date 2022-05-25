using Dignite.Cms.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Cms.Permissions
{
    public class CmsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(CmsPermissions.GroupName, L("Permission:Cms"));

            var pages = group.AddPermission(CmsPermissions.Page.Default, L("Permission:Pages"));
            pages.AddChild(CmsPermissions.Page.Create, L("Permission:Create"));
            pages.AddChild(CmsPermissions.Page.Update, L("Permission:Edit"));
            pages.AddChild(CmsPermissions.Page.Delete, L("Permission:Delete"));
            pages.AddChild(CmsPermissions.Page.SuperAuthorization, L("Permission:SuperAuthorization"));

            var sections = group.AddPermission(CmsPermissions.Section.Default, L("Permission:Section"));
            sections.AddChild(CmsPermissions.Section.Create, L("Permission:Create"));
            sections.AddChild(CmsPermissions.Section.Update, L("Permission:Edit"));
            sections.AddChild(CmsPermissions.Section.Delete, L("Permission:Delete"));

            var entries = group.AddPermission(CmsPermissions.Entry.Default, L("Permission:Entry"));
            entries.AddChild(CmsPermissions.Entry.Create, L("Permission:Create"));
            entries.AddChild(CmsPermissions.Entry.Update, L("Permission:Edit"));
            entries.AddChild(CmsPermissions.Entry.Delete, L("Permission:Delete"));
            entries.AddChild(CmsPermissions.Entry.Audit, L("Permission:Audit"));

            var users = group.AddPermission(CmsPermissions.User.Default, L("Permission:User"));
            users.AddChild(CmsPermissions.User.Create, L("Permission:Create"));
            users.AddChild(CmsPermissions.User.Update, L("Permission:Edit"));
            users.AddChild(CmsPermissions.User.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CmsResource>(name);
        }
    }
}