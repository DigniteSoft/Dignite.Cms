using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Dignite.SiteBuilding.Admin.Blazor.Menus
{
    public class SiteBuildingAdminMenuContributor : IMenuContributor
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
            var l = context.GetLocalizer<SiteBuildingResource>();

            //Add main menu items.
            var rootMenuItem = new ApplicationMenuItem(SiteBuildingAdminMenus.Prefix, displayName: l["SiteBuilding"], url: "~/site-building", icon: "fa fa-globe");
            context.Menu.AddItem(rootMenuItem);

            rootMenuItem.AddItem(new ApplicationMenuItem(
                    SiteBuildingAdminMenus.Pages,
                    l["Pages"],
                    url: "~/site-building/pages",
                    icon: "fa fa-file").RequirePermissions(SiteBuildingPermissions.Page.Default));

            rootMenuItem.AddItem(new ApplicationMenuItem(
                    SiteBuildingAdminMenus.Entries,
                    l["Sections"],
                    url: "~/site-building/sections",
                    icon: "fa fa-file-alt").RequirePermissions(SiteBuildingPermissions.Entry.Default));

            rootMenuItem.AddItem(new ApplicationMenuItem(
                    SiteBuildingAdminMenus.Users,
                    l["Users"],
                    url: "~/site-building/users",
                    icon: "fa fa-users").RequirePermissions(SiteBuildingPermissions.User.Default));

            return Task.CompletedTask;
        }
    }
}