

using System;

namespace Dignite.Cms.Admin.Entries
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
