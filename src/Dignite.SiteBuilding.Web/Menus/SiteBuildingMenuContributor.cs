using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.SiteBuilding.Web.Menus
{
    public class SiteBuildingMenuContributor : IMenuContributor
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
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(SiteBuildingMenus.Prefix, displayName: "SiteBuilding", "~/SiteBuilding", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}