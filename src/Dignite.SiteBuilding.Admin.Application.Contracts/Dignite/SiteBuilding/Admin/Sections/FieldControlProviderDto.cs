﻿
using Dignite.Abp.FieldCustomizing.FieldControls;

namespace Dignite.SiteBuilding.Admin.Sections
{
    /// <summary>
    /// 字段控件Provider
    /// </summary>
    public class FieldControlProviderDto
    {
        public FieldControlProviderDto(string formProviderName, string formDisplayName, FieldControlType fieldControlType)
        {
            Name = formProviderName;
            DisplayName = formDisplayName;
            FieldControlType = fieldControlType;
        }

        public string Name { get; }

        public string DisplayName { get; }

        public FieldControlType FieldControlType { get; }
    }
}
