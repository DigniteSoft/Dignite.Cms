using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Cms.Web.Menus
{
    public class CmsMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(CmsMenus.Prefix, displayName: "Cms", "~/Cms", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}