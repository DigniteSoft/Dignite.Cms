using AutoMapper;
using Dignite.SiteBuilding.Pages;
using Dignite.SiteBuilding.Sections;
using Dignite.SiteBuilding.Admin.Sections;
using Dignite.SiteBuilding.Users;
using Dignite.SiteBuilding.Admin.Pages;
using Dignite.SiteBuilding.Entries;
using Dignite.SiteBuilding.Admin.Entries;

namespace Dignite.SiteBuilding.Admin
{
    public class SiteBuildingAdminApplicationAutoMapperProfile : Profile
    {
        public SiteBuildingAdminApplicationAutoMapperProfile()
        {
            /**** page *****************************************/
            CreateMap<Page, PageDto>();
            CreateMap<Page, PageEditOutput>();


            /**** entry type *****************************************/
            CreateMap<Section, SectionDto>();
            CreateMap<Section, SectionEditDto>();
            CreateMap<FieldDefinition, FieldDefinitionDto>();
            CreateMap<FieldDefinition, FieldDefinitionEditDto>();


            /**** entity *****************************************/
            CreateMap<Entry, EntryDto>()
                .MapCustomizeFields()
                .ForMember(m => m.Editor, y => y.Ignore());
            CreateMap<Entry, EntryEditDto>();


            /**** user *****************************************/
            CreateMap<SiteUser, SiteUserDto>();
        }
    }
}