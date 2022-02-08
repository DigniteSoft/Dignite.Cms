using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Dignite.SiteBuilding.Pages
{
    public class IndexModel : SiteBuildingPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}