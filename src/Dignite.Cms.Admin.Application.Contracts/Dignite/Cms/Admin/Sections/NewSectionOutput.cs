
using System.Collections.Generic;

namespace Dignite.Cms.Admin.Sections
{
    public class NewSectionOutput
    {
        public NewSectionOutput()
        {
            AllFieldProviders = new List<FieldProviderDto>();
        }

        public SectionCreateDto Section { get; set; }

        /// <summary>
        /// 所有的字段控件类型集合
        /// </summary>
        public IReadOnlyList<FieldProviderDto> AllFieldProviders { get; set; }
    }
}
