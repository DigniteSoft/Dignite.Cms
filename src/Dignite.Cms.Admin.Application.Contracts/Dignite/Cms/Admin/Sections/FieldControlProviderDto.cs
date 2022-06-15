
using Dignite.Abp.FieldCustomizing.Fields;

namespace Dignite.Cms.Admin.Sections
{
    /// <summary>
    /// 字段控件Provider
    /// </summary>
    public class FieldProviderDto
    {
        public FieldProviderDto()
        {
        }

        public FieldProviderDto(string fieldProviderName, string fieldDisplayName, FieldType fieldType)
        {
            Name = fieldProviderName;
            DisplayName = fieldDisplayName;
            FieldType = fieldType;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public FieldType FieldType { get; set; }
    }
}
