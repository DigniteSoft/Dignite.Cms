using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Dignite.Abp.ObjectExtending
{
    public static class SiteBuildingModuleExtensionConfigurationDictionaryExtensions
    {
        public static ModuleExtensionConfigurationDictionary ConfigureIdentity(
            this ModuleExtensionConfigurationDictionary modules,
            Action<SiteBuildingModuleExtensionConfiguration> configureAction)
        {
            return modules.ConfigureModule(
                SiteBuildingModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}
