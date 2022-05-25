using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Sections
{
    public interface ISectionGrantRepository : IBasicRepository<SectionGrant>
    {
        Task<SectionGrant> GetAsync(Guid sectionId,Guid userId, CancellationToken cancellationToken = default);

        Task<List<SectionGrant>> GetListAsync(Guid sectionId, CancellationToken cancellationToken = default);
    }
}
