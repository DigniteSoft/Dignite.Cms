﻿
using System.Collections.Generic;

namespace Dignite.Cms.Admin.Sections
{
    public class NewSectionOutput
    {
        public NewSectionOutput()
        {
            AllFieldProviders = new List<FieldControlProviderDto>();
        }

        public SectionCreateDto Section { get; set; }

        /// <summary>
        /// 所有的字段控件类型集合
        /// </summary>
        public IReadOnlyList<FieldControlProviderDto> AllFieldProviders { get; set; }
    }
}