using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.SiteBuilding.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Dignite.SiteBuilding.Users
{
    public class EfCoreSiteUserRepository : EfCoreUserRepositoryBase<ISiteBuildingDbContext, SiteUser>, ISiteUserRepository
    {
        public EfCoreSiteUserRepository(IDbContextProvider<ISiteBuildingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<List<SiteUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf( !string.IsNullOrWhiteSpace( filter), x=>x.UserName.Contains(filter))
                .Take(maxCount)
                .ToListAsync(cancellationToken);
        }
    }
}
