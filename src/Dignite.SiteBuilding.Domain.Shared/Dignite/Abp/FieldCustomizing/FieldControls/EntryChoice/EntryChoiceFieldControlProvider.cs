
using Dignite.SiteBuilding.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing.FieldControls.EntryChoice
{
    public class EntryChoiceFieldControlProvider : FieldControlProviderBase
    {

        public const string ProviderName = "EntryChoice";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.EntryChoice"];

        public override FieldControlType ControlType => FieldControlType.Simple;

        public override void Validate(FieldControlValidateArgs args)
        {
            var configuration = new EntryChoiceConfiguration(args.FieldDefinition.Configuration);
            var entryIds = new List<Guid>();

            if (args.Value is IEnumerable<Guid>)
            {
                entryIds.AddRange((IEnumerable<Guid>)args.Value);
            }
            else if (args.Value is Guid)
            {
                entryIds.Add((Guid)args.Value);
            }

            if (configuration.Required && (args.Value == null || !entryIds.Any()))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:Required"].Value,
                        new[] { args.FieldDefinition.Name }
                        ));
            }


            if (configuration.MaxSelectLimit < entryIds.Count)
            {
                //TODO...
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["{0} 最多允许选择 {0} 个", args.FieldDefinition.DisplayName, configuration.MaxSelectLimit],
                        new[] { args.FieldDefinition.Name }
                        ));
            }

        }

        public override FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
        {
            return new EntryChoiceConfiguration(fieldConfiguration);
        }

        protected override IStringLocalizer CreateLocalizer()
        {
            return StringLocalizerFactory.Create(typeof(SiteBuildingResource));
        }
    }
}
