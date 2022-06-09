using Dignite.Abp.FileManagement;
using Volo.Abp.Modularity;

namespace Dignite.Cms.Admin
{
    [DependsOn(
        typeof(CmsApplicationContractsSharedModule),
        typeof(FileManagementApplicationContractsModule)
        )]
    public class CmsAdminApplicationContractsModule : AbpModule
    {

    }
}
