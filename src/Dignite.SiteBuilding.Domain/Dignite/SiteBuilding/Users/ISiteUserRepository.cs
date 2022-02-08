using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Dignite.SiteBuilding.Users
{
    public interface ISiteUserRepository : IBasicRepository<SiteUser, Guid>, IUserRepository<SiteUser>
    {
        Task<List<SiteUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default);
    }
}