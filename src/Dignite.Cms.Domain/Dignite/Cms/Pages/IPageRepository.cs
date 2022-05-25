using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Pages
{
    public interface IPageRepository : IBasicRepository<Page, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ignoredId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> PathExistsAsync( string path, Guid? ignoredId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Page> FindByPathAsync(string path, CancellationToken cancellationToken = default);

        Task<List<Page>> GetListAsync(Guid? parentId, CancellationToken cancellationToken = default);
    }
}