
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Sections
{
    public interface ISectionAppService : IApplicationService
    {
        Task<SectionDto> FindByNameAsync(string name);
        Task<SectionDto> GetAsync(Guid id);
    }
}
