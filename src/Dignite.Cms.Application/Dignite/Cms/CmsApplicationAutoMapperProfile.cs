using AutoMapper;
using Dignite.Cms.Sections;
using Dignite.Cms.Pages;
using Dignite.Cms.Users;
using Dignite.Cms.Entries;

namespace Dignite.Cms
{
    public class CmsApplicationAutoMapperProfile : Profile
    {
        public CmsApplicationAutoMapperProfile()
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