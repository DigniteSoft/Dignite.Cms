using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Entries
{
    public interface IEntryRepository : IBasicRepository<Entry, Guid>
    {
        Task<bool> AnyAsync( Guid pageId, string slug, Guid? ignoredId = null, CancellationToken cancellationToken = default);

        Task<List<Entry>> GetListAsync(
            Guid sectionId, Guid? pageId=null, Guid? creatorId = null, 
            EntryAuditStatus? auditedStatus = null, bool? IsActive = null,            
            int maxResultCount = int.MaxValue,
            int skipCount = 0, 
            CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(
             Guid sectionId, Guid? pageId = null, Guid? creatorId = null,  
             EntryAuditStatus? auditedStatus = null, bool? IsActive = null, 
             CancellationToken cancellationToken = default
            );

        Task<List<Entry>> GetListAsync(Guid sectionId, IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        Task<Entry> FindBySlugAsync( Guid pageId, string slug, CancellationToken cancellationToken = default);

        Task<Entry> FindPrevAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Entry> FindNextAsync(Guid id, CancellationToken cancellationToken = default);

        Task<int> GetMaxPositionAsync(Guid sectionId, Guid pageId, CancellationToken cancellationToken = default);
    }
}
