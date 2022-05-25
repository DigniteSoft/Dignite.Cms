
using Dignite.Abp.FieldCustomizing.FieldControls;

namespace Dignite.Cms.Admin.Sections
{
    /// <summary>
    /// 字段控件Provider
    /// </summary>
    public class FieldControlProviderDto
    {
        public FieldControlProviderDto()
        {
        }

        public FieldControlProviderDto(string formProviderName, string formDisplayName, FieldControlType fieldControlType)
        {
            Name = formProviderName;
            DisplayName = formDisplayName;
            FieldControlType = fieldControlType;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public FieldControlType FieldControlType { get; set; }
    }
}
