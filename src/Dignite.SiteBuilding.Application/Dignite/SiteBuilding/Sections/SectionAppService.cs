using System;
using System.Threading.Tasks;

namespace Dignite.SiteBuilding.Sections
{
    public class SectionAppService : SiteBuildingAppService, ISectionAppService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionAppService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SectionDto> FindByNameAsync(string name)
        {
            return ObjectMapper.Map<Section, SectionDto>(
                await _sectionRepository.FindByNameAsync( name)
                );
        }
        public async Task<SectionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Section, SectionDto>(
                await _sectionRepository.GetAsync(id)
                );
        }
    }
}
