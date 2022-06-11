using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Dignite.Cms.Admin.Blazor.Menus
{
    public class CmsAdminMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<CmsResource>();

            //Add main menu items.
            var rootMenuItem = new ApplicationMenuItem(
                CmsAdminMenus.Prefix, 
                displayName: l["Cms"], 
                url: "~/cms/admin", 
                icon: "fa fa-file-o");
            rootMenuItem.RequirePermissions(CmsPermissions.Entry.Default);
            context.Menu.AddItem(rootMenuItem);


            var cms = new ApplicationMenuItem(
                    "cms",
                    l["Cms"]
                );
            rootMenuItem.AddItem(cms);

            cms.AddItem(new ApplicationMenuItem(
                    CmsAdminMenus.Pages,
                    l["Pages"],
                    url: "~/cms/admin/pages",
                    icon: "fa fa-file").RequirePermissions(CmsPermissions.Page.Default));

            cms.AddItem(new ApplicationMenuItem(
                    CmsAdminMenus.Entries,
                    l["Sections"],
                    url: "~/cms/admin/sections",
                    icon: "fa fa-file-alt").RequirePermissions(CmsPermissions.Entry.Default));
            /*
            cms.AddItem(new ApplicationMenuItem(
                    CmsAdminMenus.Users,
                    l["Users"],
                    url: "~/cms/admin/users",
                    icon: "fa fa-users").RequirePermissions(CmsPermissions.User.Default));
            */

            return Task.CompletedTask;
        }
    }
}