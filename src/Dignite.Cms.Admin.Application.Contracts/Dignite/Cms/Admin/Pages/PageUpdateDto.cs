

using Volo.Abp.Domain.Entities;

namespace Dignite.Cms.Admin.Pages
{
    public class PageUpdateDto : PageCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
