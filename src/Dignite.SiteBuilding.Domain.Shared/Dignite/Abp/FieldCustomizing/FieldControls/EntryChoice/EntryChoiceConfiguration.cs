
using System;

namespace Dignite.Abp.FieldCustomizing.FieldControls.EntryChoice
{
    public class EntryChoiceConfiguration: FieldControlConfigurationBase
    {

        /// <summary>
        /// 版块
        /// </summary>
        public Guid PageId
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<Guid>(EntryChoiceConfigurationNames.PageId);
            set => _fieldControlConfiguration.SetConfiguration(EntryChoiceConfigurationNames.PageId, value);
        }

        /// <summary>
        /// 版块
        /// </summary>
        public Guid SectionId
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<Guid>(EntryChoiceConfigurationNames.SectionId);
            set => _fieldControlConfiguration.SetConfiguration(EntryChoiceConfigurationNames.SectionId, value);
        }



        /// <summary>
        /// 条目的最多可选数量
        /// </summary>
        public int MaxSelectLimit
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(EntryChoiceConfigurationNames.MaxSelectLimit, 1);
            set => _fieldControlConfiguration.SetConfiguration(EntryChoiceConfigurationNames.MaxSelectLimit, value);
        }


        public EntryChoiceConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
