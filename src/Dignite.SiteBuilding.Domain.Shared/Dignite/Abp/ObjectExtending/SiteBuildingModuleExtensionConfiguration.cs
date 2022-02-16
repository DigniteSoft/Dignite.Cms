using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Dignite.Abp.ObjectExtending
{
    public class SiteBuildingModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public SiteBuildingModuleExtensionConfiguration ConfigureUser(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                SiteBuildingModuleExtensionConsts.EntityNames.Page,
                configureAction
            );
        }
    }
}