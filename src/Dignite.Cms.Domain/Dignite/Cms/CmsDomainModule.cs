using Dignite.Abp.FileManagement;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Cms
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(CmsDomainSharedModule),
        typeof(FileManagementDomainModule)
    )]
    public class CmsDomainModule : AbpModule
    {

    }
}
