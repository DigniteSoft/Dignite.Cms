using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Dignite.Abp.ObjectExtending
{
    public class CmsModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public CmsModuleExtensionConfiguration ConfigureUser(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                CmsModuleExtensionConsts.EntityNames.Page,
                configureAction
            );
        }
    }
}