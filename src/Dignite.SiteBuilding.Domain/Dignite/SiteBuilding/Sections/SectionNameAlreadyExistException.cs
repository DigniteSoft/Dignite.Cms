using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.SiteBuilding.Sections
{
    [Serializable]
    public class SectionNameAlreadyExistException : BusinessException
    {
        public SectionNameAlreadyExistException([NotNull]string name)
        {
            Code = SiteBuildingErrorCodes.Sections.NameAlreadyExist;
            WithData(nameof(Section.Name), name);
        }
    }
}
