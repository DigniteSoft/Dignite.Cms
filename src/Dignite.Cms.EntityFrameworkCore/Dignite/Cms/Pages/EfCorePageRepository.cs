using Dignite.Cms.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.Pages
{
    public class EfCorePageRepository : EfCoreRepository<ICmsDbContext, Page,Guid>, IPageRepository
    {
        public EfCorePageRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }


        public async Task<bool> PathExistsAsync([NotNull]string path, Guid? ignoredId = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .WhereIf(ignoredId != null, ct => ct.Id != ignoredId)
                       .AnyAsync(ct => ct.Path == path, GetCancellationToken(cancellationToken));
        }


        public async Task<Page> FindByPathAsync(string path, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).SingleOrDefaultAsync(ct => ct.Path == path, GetCancellationToken(cancellationToken));
        }

        public async Task<List<Page>> GetListAsync(Guid? parentId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p=>p.ParentId==parentId)
                .OrderBy(p=>p.Position)
                .ToListAsync( GetCancellationToken(cancellationToken));
        }
    }
}
