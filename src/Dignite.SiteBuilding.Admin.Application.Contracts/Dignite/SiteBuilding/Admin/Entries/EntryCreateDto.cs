

using System;

namespace Dignite.SiteBuilding.Admin.Entries
{
    public class EntryCreateDto : EntryCreateOrUpdateDtoBase
    {
        public EntryCreateDto()
        {
        }

        public EntryCreateDto(Guid sectionId) : base(sectionId)
        {
        }
    }
}
