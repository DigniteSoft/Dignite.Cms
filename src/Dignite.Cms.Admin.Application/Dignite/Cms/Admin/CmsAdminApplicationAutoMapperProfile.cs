using AutoMapper;
using Dignite.Cms.Pages;
using Dignite.Cms.Sections;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Users;
using Dignite.Cms.Entries;
using Dignite.Cms.Admin.Entries;

namespace Dignite.Cms.Admin
{
    public class CmsAdminApplicationAutoMapperProfile : Profile
    {
        public CmsAdminApplicationAutoMapperProfile()
        {
            /**** page *****************************************/
            CreateMap<Page, PageDto>()
            .MapExtraProperties()
                .ForMember(m => m.Name, y => y.Ignore())
                .ForMember(m => m.Children, y => y.Ignore());


            /**** entry type *****************************************/
            CreateMap<Section, SectionDto>();
            CreateMap<Section, SectionUpdateDto>();
            CreateMap<FieldDefinition, FieldDefinitionDto>();
            CreateMap<FieldDefinition, FieldDefinitionEditDto>();


            /**** entity *****************************************/
            CreateMap<Entry, EntryDto>()
                .MapCustomizeFields()
                .ForMember(m => m.Page, y => y.Ignore())
                .ForMember(m => m.Editor, y => y.Ignore());
            CreateMap<Entry, EntryUpdateDto>()
                .ForMember(m => m.CustomizedFieldFiles, y => y.Ignore());


            /**** user *****************************************/
            CreateMap<SiteUser, SiteUserDto>();
        }
    }
}