using AutoMapper;
using Dignite.SiteBuilding.Sections;
using Dignite.SiteBuilding.Pages;
using Dignite.SiteBuilding.Users;
using Dignite.SiteBuilding.Entries;

namespace Dignite.SiteBuilding
{
    public class SiteBuildingApplicationAutoMapperProfile : Profile
    {
        public SiteBuildingApplicationAutoMapperProfile()
        {
            /**** site *****************************************/
            CreateMap<Page, PageDto>()
                .ForMember(m => m.Name, y => y.Ignore())
                .ForMember(m => m.Children, y => y.Ignore());


            /**** entry type *****************************************/
            CreateMap<Section, SectionDto>();
            CreateMap<FieldDefinition, FieldDefinitionDto>();


            /**** entity *****************************************/
            CreateMap<Entry, EntryDto>()
                .MapCustomizeFields()
                .ForMember(m => m.Page, y => y.Ignore())
                .ForMember(m => m.Editor, y => y.Ignore());

            /**** user *****************************************/
            CreateMap<SiteUser, SiteUserDto>();
        }
    }
}