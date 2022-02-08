using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Admin.Sections
{
    public class GetSectionsInput : PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
