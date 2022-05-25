using Dignite.Cms.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.Entries
{
    public class EfCoreEntryRepository : EfCoreRepository<ICmsDbContext, Entry,Guid>, IEntryRepository
    {

        public EfCoreEntryRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }


        public async Task<bool> AnyAsync(Guid pageId, string slug, Guid? ignoredId = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .WhereIf(ignoredId != null, ct => ct.Id != ignoredId)
                       .AnyAsync(e => e.PageId== pageId && e.Slug == slug, GetCancellationToken(cancellationToken));
        }


        public async Task<List<Entry>> GetListAsync(
            Guid sectionId, Guid? pageId = null, Guid? creatorId = null,
            EntryAuditStatus? auditedStatus = null, bool? IsActive = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync(sectionId, pageId, creatorId, auditedStatus, IsActive))
                .OrderByDescending(e => e.CreationTime)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetCountAsync(
             Guid sectionId, Guid? pageId = null, Guid? creatorId = null,
             EntryAuditStatus? auditedStatus = null, bool? IsActive = null,
             CancellationToken cancellationToken = default
            )
        {
            return await (await GetQueryableAsync(sectionId, pageId,creatorId,auditedStatus,IsActive))
                .CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Entry>> GetListAsync(Guid sectionId, IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(e => e.SectionId == sectionId && ids.Contains(e.Id))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<Entry> FindBySlugAsync(Guid pageId, string slug, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(e => e.PageId == pageId && e.Slug == slug, GetCancellationToken(cancellationToken));

                }

        public async Task<Entry> FindPrevAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var currentEntry = await dbSet.FirstAsync(m => m.Id == id, GetCancellationToken(cancellationToken));
            return await dbSet
                    .Where(m => m.SectionId == currentEntry.SectionId && m.PageId==currentEntry.PageId && m.CreationTime < currentEntry.CreationTime && m.IsActive && m.AuditStatus == EntryAuditStatus.Allowed)
                    .OrderByDescending(e => e.CreationTime)
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));           

        }

        public async Task<Entry> FindNextAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var currentEntry = await dbSet.FirstAsync(m => m.Id == id, GetCancellationToken(cancellationToken));
            return await dbSet
                    .Where(m => m.SectionId == currentEntry.SectionId && m.PageId == currentEntry.PageId && m.CreationTime > currentEntry.CreationTime && m.IsActive && m.AuditStatus == EntryAuditStatus.Allowed)
                    .OrderBy(e => e.CreationTime)
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetMaxPositionAsync(Guid sectionId, Guid pageId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).Where(e => e.SectionId == sectionId && e.PageId == pageId)
                .Select(x => x.Position)
                .DefaultIfEmpty(0)
                .MaxAsync(GetCancellationToken(cancellationToken));
        }



        private async Task<IQueryable<Entry>> GetQueryableAsync(
             Guid sectionId, Guid? pageId = null, Guid? creatorId = null,
             EntryAuditStatus? auditedStatus = null, bool? IsActive = null)
        {
            return (await GetDbSetAsync()).Where(e => e.SectionId == sectionId)
                .WhereIf(pageId.HasValue, e => e.PageId == pageId.Value)
                .WhereIf(creatorId.HasValue, e => e.CreatorId == creatorId.Value)
                .WhereIf(auditedStatus.HasValue, e => e.AuditStatus == auditedStatus.Value)
                .WhereIf(IsActive.HasValue, e => e.IsActive == IsActive.Value);
        }
    }
}
