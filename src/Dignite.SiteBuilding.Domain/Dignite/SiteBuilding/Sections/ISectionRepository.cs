using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.SiteBuilding.Sections
{
    public interface ISectionRepository : IBasicRepository<Section, Guid>
    {
        Task<bool> NameExistsAsync(string name, Guid? ignoredId = null, CancellationToken cancellationToken = default);


        Task<Section> FindByNameAsync( string name, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<List<Section>> GetListAsync(
            string filter,
            Guid? authorizerId,
            int maxResultCount = int.MaxValue,
            int skipCount = 0, 
            CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(
            string filter,
            Guid? authorizerId,
            CancellationToken cancellationToken = default);

        Task<bool> IsAuthorizedAsync(Guid id,Guid pageId,Guid userId);
    }
}
