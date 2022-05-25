using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Dignite.Abp.ObjectExtending
{
    public static class CmsModuleExtensionConfigurationDictionaryExtensions
    {
        public static ModuleExtensionConfigurationDictionary ConfigureIdentity(
            this ModuleExtensionConfigurationDictionary modules,
            Action<CmsModuleExtensionConfiguration> configureAction)
        {
            return modules.ConfigureModule(
                CmsModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}
