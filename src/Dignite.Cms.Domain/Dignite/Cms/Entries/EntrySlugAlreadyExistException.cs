using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Entries
{
    [Serializable]
    public class EntrySlugAlreadyExistException : BusinessException
    {
        public EntrySlugAlreadyExistException( [NotNull] Guid pageId, [NotNull] string slug)
        {
            Code = CmsErrorCodes.Entries.SlugAlreadyExist;
            WithData(nameof(Entry.PageId), pageId);
            WithData(nameof(Entry.Slug), slug);
        }
    }
}
