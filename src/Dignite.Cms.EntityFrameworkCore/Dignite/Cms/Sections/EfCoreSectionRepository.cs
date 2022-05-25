using Dignite.Cms.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.Sections
{
    public class EfCoreSectionRepository : EfCoreRepository<ICmsDbContext, Section,Guid>, ISectionRepository
    {

        public EfCoreSectionRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }


        public async Task<bool> NameExistsAsync(string name, Guid? ignoredId = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .WhereIf(ignoredId != null, ct => ct.Id != ignoredId)
                       .AnyAsync(ct => ct.Name == name, GetCancellationToken(cancellationToken));
        }

        public async Task<Section> FindByNameAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await ((await GetQueryableAsync())
                .IncludeDetails(includeDetails))
                .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<List<Section>> GetListAsync(
            string filter,
            Guid? authorizerId,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync(filter,authorizerId))
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetCountAsync(
            string filter,
            Guid? authorizerId,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync(filter, authorizerId))
                .CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<bool> IsAuthorizedAsync(Guid id, Guid pageId, Guid userId)
        {
            var result = await (await GetDbContextAsync()).SectionGrants.FirstOrDefaultAsync(auth=>
                auth.SectionId==id && auth.UserId==userId
            );
            if (result == null || !result.PageIds.Any())
                return false;
            else
            {
                var page = await (await GetDbContextAsync()).Pages.FirstAsync(p=>p.Id==pageId);
                var grantedPages = await (await GetDbContextAsync()).Pages.Where(p => result.PageIds.Contains(p.Id)).ToListAsync();
                if(grantedPages.Any(gp=>gp.Path.EnsureEndsWith('/').StartsWith(page.Path.EnsureEndsWith('/'), StringComparison.OrdinalIgnoreCase)))
                    return true;
                else
                    return false;
            }
        }

        public override async Task<IQueryable<Section>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        private async Task< IQueryable<Section>> GetQueryableAsync(string filter, Guid? authorizerId)
        {
            return (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrEmpty(), et => et.DisplayName.Contains(filter))
                .WhereIf(authorizerId.HasValue, et => et.Authorizers.Any(auth => auth.UserId == authorizerId.Value));
        }
    }
}
