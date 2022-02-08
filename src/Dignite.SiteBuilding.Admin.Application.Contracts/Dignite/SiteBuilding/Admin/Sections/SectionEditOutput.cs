
using System.Collections.Generic;

namespace Dignite.SiteBuilding.Admin.Sections
{
    public class SectionEditOutput
    {
        public SectionEditDto Section { get; set; }

        /// <summary>
        /// 所有的字段控件类型集合
        /// </summary>
        public IReadOnlyList<FieldControlProviderDto> AllFieldProviders { get; set; }
    }
}
