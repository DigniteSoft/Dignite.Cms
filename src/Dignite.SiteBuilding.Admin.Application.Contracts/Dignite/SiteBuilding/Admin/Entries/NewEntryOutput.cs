using Dignite.SiteBuilding.Sections;

namespace Dignite.SiteBuilding.Admin.Entries
{
    public class NewEntryOutput
    {
        public SectionDto Section { get; set; }

        public EntryCreateDto Entry { get; set; }
    }
}
