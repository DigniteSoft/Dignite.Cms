using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Pages
{
    [Serializable]
    public class PagePathAlreadyExistException : BusinessException
    {
        public PagePathAlreadyExistException( [NotNull] string path)
        {
            Code = CmsErrorCodes.Pages.PathAlreadyExist;
            WithData(nameof(Page.Path), path);
        }
    }
}
