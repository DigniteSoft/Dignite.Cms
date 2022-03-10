using Dignite.SiteBuilding.Sections;

namespace Dignite.SiteBuilding.Admin.Entries
{
    public class EditEntryOutput
    {
        public SectionDto Section { get; set; }

        public EntryUpdateDto Entry { get; set; }
    }
}
