
using System;

namespace Dignite.Abp.FieldCustomizing.Fields.EntryChoice
{
    public class EntryChoiceConfiguration: FieldConfigurationBase
    {

        /// <summary>
        /// 版块
        /// </summary>
        public Guid PageId
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<Guid>(EntryChoiceConfigurationNames.PageId);
            set => _fieldConfiguration.SetConfiguration(EntryChoiceConfigurationNames.PageId, value);
        }

        /// <summary>
        /// 版块
        /// </summary>
        public Guid SectionId
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<Guid>(EntryChoiceConfigurationNames.SectionId);
            set => _fieldConfiguration.SetConfiguration(EntryChoiceConfigurationNames.SectionId, value);
        }



        /// <summary>
        /// 条目的最多可选数量
        /// </summary>
        public int MaxSelectLimit
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(EntryChoiceConfigurationNames.MaxSelectLimit, 1);
            set => _fieldConfiguration.SetConfiguration(EntryChoiceConfigurationNames.MaxSelectLimit, value);
        }


        public EntryChoiceConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
