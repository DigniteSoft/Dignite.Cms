using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Sections
{
    public class GetSectionsInput : PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
