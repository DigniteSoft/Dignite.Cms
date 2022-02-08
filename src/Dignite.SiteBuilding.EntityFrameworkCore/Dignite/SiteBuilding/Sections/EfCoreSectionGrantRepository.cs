using Dignite.SiteBuilding.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.SiteBuilding.Sections
{
    public class EfCoreSectionGrantRepository : EfCoreRepository<ISiteBuildingDbContext, SectionGrant>, ISectionGrantRepository
    {
        public EfCoreSectionGrantRepository(
            IDbContextProvider<ISiteBuildingDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }


        public async Task<SectionGrant> GetAsync(Guid sectionId, Guid userId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .Where(auth=>auth.SectionId== sectionId && auth.UserId==userId)
                       .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }


        public async Task<List<SectionGrant>> GetListAsync(Guid sectionId, CancellationToken cancellationToken = default)
        {
            return (await (await GetDbSetAsync()).Where(auth => auth.SectionId == sectionId)
                .ToListAsync(GetCancellationToken(cancellationToken)));
        }
    }
}
