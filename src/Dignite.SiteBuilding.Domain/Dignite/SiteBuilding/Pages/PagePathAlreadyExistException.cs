using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.SiteBuilding.Pages
{
    [Serializable]
    public class PagePathAlreadyExistException : BusinessException
    {
        public PagePathAlreadyExistException( [NotNull] string path)
        {
            Code = SiteBuildingErrorCodes.Pages.PathAlreadyExist;
            WithData(nameof(Page.Path), path);
        }
    }
}
