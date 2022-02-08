using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.SiteBuilding.Entries
{
    [Serializable]
    public class EntrySlugAlreadyExistException : BusinessException
    {
        public EntrySlugAlreadyExistException( [NotNull] Guid pageId, [NotNull] string slug)
        {
            Code = SiteBuildingErrorCodes.Entries.SlugAlreadyExist;
            WithData(nameof(Entry.PageId), pageId);
            WithData(nameof(Entry.Slug), slug);
        }
    }
}
